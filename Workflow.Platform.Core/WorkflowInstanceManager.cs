/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowInstanceManager.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-12
 *   职    责   ：   工作流实例管理器
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-12        1.0.0.0        余树杰        初版　 
 *   2015-05-19        1.1.0.0        余树杰        增加催单功能 
 *   2015-08-14        1.1.0.1        余树杰        增加工作流平台日志记录
 *   
 *   
 *******************************************************************************/

using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.DurableInstancing;
using System.Threading;
using Workflow.Domain.DomainObjects;
using Workflow.Domain.Repositories;
using Workflow.Extensions;
using Workflow.Platform.Common.Entities;
using Workflow.Platform.Common.Helper;
using Workflow.Platform.Common.Interface;
using Workflow.Platform.Core.Factory;

namespace Workflow.Platform.Core
{
    /// <summary>
    /// 工作流实例管理器
    /// </summary>
    public class WorkflowInstanceManager
    {
        #region 属性
        /// <summary>
        /// 当前运行的流程类型
        /// </summary>
        public Activity CurrentWorkflow { get; private set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 流程类库的存放目录
        /// </summary>
        public string WorkflowLibraryPath { get; set; }

        /// <summary>
        /// 流程环节信息仓储
        /// </summary>
        public IActivityInfoRepository ActivityRepository { get; set; }

        /// <summary>
        /// 流程日志仓储
        /// </summary>
        public IWorkflowInstanceLogRepository WorkflowInstanceLogRepository { get; set; }

        /// <summary>
        /// 用户信息提供者
        /// </summary>
        public IUser UserProvider { get; set; }

        /// <summary>
        /// 催单服务
        /// </summary>
        public IHastenService HastenService { get; set; }

        /// <summary>
        /// 定义工作流闲置并可持久化时的操作
        /// </summary>
        public Func<WorkflowApplicationIdleEventArgs, PersistableIdleAction> PersistableIdle
        {
            get
            {
                return new Func<WorkflowApplicationIdleEventArgs, PersistableIdleAction>(e => {
                    this.SaveWorkflowInstanceLogs();
                    return PersistableIdleAction.Unload; 
                });
            }
        }

        /// <summary>
        /// 定义流程结束时的操作
        /// </summary>
        public Action<WorkflowApplicationCompletedEventArgs> Completed
        {
            get
            {
                return new Action<WorkflowApplicationCompletedEventArgs>(e =>
                {
                    this.SaveWorkflowInstanceLogs();
                });
            }
        }

        /// <summary>
        /// 工作流持久化对象
        /// </summary>
        private SqlWorkflowInstanceStore _instanceStore;
        public SqlWorkflowInstanceStore InstanceStore
        {
            get
            {
                if (null == _instanceStore)
                {
                    _instanceStore = new SqlWorkflowInstanceStore(this.ConnectionString);
                    _instanceStore.Promote(this.CurrentWorkflow.DisplayName, Workflow.Extensions.PersistenceConfig.PromoteProperties, null);//设置自定义数据与工作流实例关联
                    _instanceStore.InstanceCompletionAction = InstanceCompletionAction.DeleteNothing;//设置工作流实例完成后保留实例相关数据
                    InstanceHandle handle = _instanceStore.CreateInstanceHandle();
                    InstanceView view = _instanceStore.Execute(handle, new CreateWorkflowOwnerCommand(), TimeSpan.FromSeconds(60));
                    handle.Free();
                    _instanceStore.DefaultInstanceOwner = view.InstanceOwner;
                }
                return _instanceStore;
            }
        }

        /// <summary>
        /// 定义流程实例的执行出现异常时的操作
        /// </summary>
        public Func<WorkflowApplicationUnhandledExceptionEventArgs, UnhandledExceptionAction> OnUnhandledException
        {
            get
            {
                return new Func<WorkflowApplicationUnhandledExceptionEventArgs, UnhandledExceptionAction>(e =>
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Workflow Handle Exception Message:{0}\r\nStackTrace:{1}", 
                        e.UnhandledException.Message, e.UnhandledException.StackTrace), "Workflow Application");
                    WorkflowLogger.Error(e.UnhandledException);
                    return UnhandledExceptionAction.Abort;
                });
            }
        }
        #endregion 属性

        #region 构造函数
        /// <summary>
        /// 初始化工作流宿主应用程序的新实例
        /// </summary>
        /// <param name="connectionString">保存工作流数据的数据库连接字符串</param>
        /// <param name="workflowLibraryPath">流程类库的存放目录</param>
        /// <param name="workflowInstanceLogRepository">流程日志仓储</param>
        public WorkflowInstanceManager(string connectionString, string workflowLibraryPath, IActivityInfoRepository activityRepository,
                                       IWorkflowInstanceLogRepository workflowInstanceLogRepository, IUser userProvider, IHastenService hastenSvc)
        {
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(workflowLibraryPath))
            {
                WorkflowLogger.Warn("WorkflowInstanceManager:connectionString or workflowLibraryPath can not be null or empty.");
                throw new ArgumentNullException("WorkflowInstanceManager:connectionString or workflowLibraryPath can not be null or empty.");
            }
            this.ConnectionString = connectionString;
            this.WorkflowLibraryPath = workflowLibraryPath;
            this.ActivityRepository = activityRepository;
            this.WorkflowInstanceLogRepository = workflowInstanceLogRepository;
            this.UserProvider = userProvider;
            this.HastenService = hastenSvc;
        }
        #endregion 构造函数

        #region 流程实例的启动及处理
        /// <summary>
        /// 创建工作流宿主应用程序
        /// </summary>
        /// <param name="workflowFullName">流程类型的完全限定名称</param>
        /// <param name="paramDictionary">传递给工作流根活动的参数</param>
        /// <param name="finished">工作流执行完毕或卸载时的操作</param>
        /// <returns></returns>
        public WorkflowApplication Build(string workflowFullName, IDictionary<string, object> paramDictionary, Action<WorkflowApplicationEventArgs> finished)
        {
            this.CurrentWorkflow = (Activity)WorkflowFactory.Create(this.WorkflowLibraryPath, workflowFullName);
            WorkflowApplication app = new WorkflowApplication(this.CurrentWorkflow);
            if (null != paramDictionary)
            {//启动工作流的一个新实例时执行
                app = new WorkflowApplication(this.CurrentWorkflow, paramDictionary);
            }
            app.InstanceStore = this.InstanceStore;
            app.Unloaded = finished;
            app.PersistableIdle = this.PersistableIdle;
            app.Completed = this.Completed;
            app.OnUnhandledException = this.OnUnhandledException;
            app.Extensions.Add(new SaveRunningInstanceData());
            app.Extensions.Add(this.ActivityRepository);
            app.Extensions.Add(this.WorkflowInstanceLogRepository);
            //当前版本暂不支持这两个扩展，下一个迭代支持
            //app.Extensions.Add(this.UserProvider);
            //app.Extensions.Add(this.HastenService);
            return app;
        }

        /// <summary>
        /// 启动工作流的一个新实例
        /// </summary>
        /// <param name="startInfo">发起流程所需参数实体</param>
        /// <returns></returns>
        public string Start(WorkflowStartInfo startInfo)
        {
            AutoResetEvent finished = new AutoResetEvent(false);
            IDictionary<string, object> paramDictionary = new Dictionary<string, object>
            {//在工作流定义的根Activity上，必须定义以下参数且名称一致
                { "instanceName", startInfo.InstanceName },
                { "createUserID", startInfo.CreateUserID },
                { "createUserName", startInfo.CreateUserName },
                { "nextActivityID", startInfo.NextActivityID },
                { "nextUserID", startInfo.NextUserID },
                { "nextUserName", startInfo.NextUserName },
                { "comment", startInfo.Comment },
                { "basicBusinessInfo", startInfo.BasicBusinessInfo }
            };
            try
            {
                WorkflowApplication app = Build(startInfo.WorkflowFullName, paramDictionary, e => { finished.Set(); });
                app.Run();
                finished.WaitOne();
                return app.Id.ToString();
            }
            catch (Exception ex)
            {
                WorkflowLogger.Error(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 提交至下一环节处理
        /// </summary>
        /// <param name="submitInfo">提交流程所需参数实体</param>
        public void Submit(WorkflowSubmitInfo submitInfo)
        {
            var paramDictionary = new Dictionary<string, object>
            {
                { "nextActivityID", submitInfo.NextActivityID },
                { "nextUserID", submitInfo.NextUserID },
                { "nextUserName", submitInfo.NextUserName },
                { "comment", submitInfo.Comment },
                { "basicBusinessInfo", submitInfo.BasicBusinessInfo },
                { "flowOperation", submitInfo.FlowOperation }
            };
            AutoResetEvent finished = new AutoResetEvent(false);
            WorkflowApplication app = Build(submitInfo.WorkflowFullName, null, e => { finished.Set(); });
            try
            {
                app.Load(new Guid(submitInfo.InstanceID));
                app.ResumeBookmark(submitInfo.InstanceID, paramDictionary);//从书签处重新激活流程并传递参数
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Message:{0}\r\nStackTrace:{1}",
                    ex.Message, ex.StackTrace), "ReloadWorkflowInstance");
                WorkflowLogger.Error(ex);
            }
            finished.WaitOne();
        }

        /// <summary>
        /// 获取实例的流程日志记录
        /// </summary>
        /// <param name="instanceID">流程实例ID</param>
        public List<WorkflowInstanceLog> GetWorkflowInstanceLogs(string instanceID)
        {
            try
            {
                List<WorkflowInstanceLog> logs = this.WorkflowInstanceLogRepository.GetWorkflowInstanceLogs(instanceID).ToList();
                return logs;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Message:{0}\r\nStackTrace:{1}",
                    ex.Message, ex.StackTrace), "GetWorkflowInstanceLogs");
                WorkflowLogger.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 保存流程日志记录
        /// </summary>
        private void SaveWorkflowInstanceLogs()
        {
            try
            {
                this.WorkflowInstanceLogRepository.Commit();//流程结束时保存流程日志记录
            }
            catch (Exception ex)
            {
                this.WorkflowInstanceLogRepository.Rollback();//流程结束时撤销流程日志记录
                System.Diagnostics.Debug.WriteLine(string.Format("Message:{0}\r\nStackTrace:{1}",
                    ex.Message, ex.StackTrace), "SaveWorkflowInstanceLogs");
                WorkflowLogger.Error(ex);
            }
        }
        #endregion 流程实例的启动及处理

        #region 催单
        /// <summary>
        /// 执行催单服务
        /// </summary>
        /// <param name="hastenInfo">流程执行催单服务所需参数</param>
        public void Hasten(WorkflowHastenInfo hastenInfo)
        {
            var paramDictionary = new Dictionary<string, object>
            {
                { "hastenLevelTriggerValue", hastenInfo.HastenLevelTriggerValue }
            };
            AutoResetEvent finished = new AutoResetEvent(false);
            WorkflowApplication app = Build(hastenInfo.WorkflowFullName, null, e => { finished.Set(); });
            try
            {
                app.Load(new Guid(hastenInfo.InstanceID));
                app.ResumeBookmark(hastenInfo.InstanceID, paramDictionary);//从书签处重新激活流程并传递参数
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Message:{0}\r\nStackTrace:{1}",
                    ex.Message, ex.StackTrace), "ReloadWorkflowInstance");
                WorkflowLogger.Error(ex);
                throw;//这里需要向上抛出异常，让HastenJob捕获并撤销对HastenTask的删除操作
            }
            finished.WaitOne();
        }
        #endregion 催单
    }
}
