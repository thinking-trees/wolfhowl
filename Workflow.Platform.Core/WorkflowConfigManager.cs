/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowConfigManager.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-19
 *   职    责   ：   工作流配置管理器
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-19        1.0.0.0        余树杰        初版　 
 *   2015-08-14        1.0.0.1        余树杰        增加工作流平台日志记录     
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Workflow.Domain.DomainObjects;
using Workflow.Domain.Repositories;
using Workflow.Platform.Common.Helper;
using Workflow.Platform.Core.Common;

namespace Workflow.Platform.Core
{
    /// <summary>
    /// 工作流配置管理器
    /// </summary>
    public class WorkflowConfigManager
    {
        #region 属性
        /// <summary>
        /// 流程类型配置信息仓储
        /// </summary>
        public IWorkflowInfoRepository WorkflowInfoRepository { get; set; }

        /// <summary>
        /// 流程环节配置信息仓储
        /// </summary>
        public IActivityInfoRepository ActivityInfoRepository { get; set; }

        /// <summary>
        /// 流程角色/用户信息仓储
        /// </summary>
        public IWorkflowRoleRepository WorkflowRoleRepository { get; set; }
        #endregion 属性

        #region 构造函数
        public WorkflowConfigManager(IWorkflowInfoRepository wfInfoRepository, IActivityInfoRepository actInfoRepository, IWorkflowRoleRepository wfRoleRepository)
        {
            this.WorkflowInfoRepository = wfInfoRepository;
            this.ActivityInfoRepository = actInfoRepository;
            this.WorkflowRoleRepository = wfRoleRepository;
        }
        #endregion 构造函数

        #region 流程配置管理
        #region 初始化
        /// <summary>
        /// 初始化流程信息
        /// </summary>
        /// <param name="workflowLibraryPath">流程类库的存放目录</param>
        /// <param name="workflowFullName">流程类型的完全限定名称</param>
        public bool Init(string workflowLibraryPath, string workflowFullName)
        {
            WorkflowLibraryResolver reslover = new WorkflowLibraryResolver(workflowLibraryPath);
            XDocument workflowXaml = null;
            bool isSuccess = false;
            try
            {
                workflowXaml = reslover.GetWorkflowXaml(workflowFullName);
                int maxWfID = 0;
                this.InitWorkflowInfo(workflowXaml, workflowFullName, out maxWfID);
                this.InitActivities(workflowXaml, maxWfID);
                this.ActivityInfoRepository.Commit();
                isSuccess = true;
            }
            catch (InvalidOperationException ex)
            {
                this.ActivityInfoRepository.Rollback();
                System.Diagnostics.Debug.WriteLine(string.Format("Message:{0}\r\nStackTrace:{1}", 
                    ex.Message, ex.StackTrace), "Workflow Initialize");
                WorkflowLogger.Error(ex);
            }
            catch (Exception ex)
            {
                this.ActivityInfoRepository.Rollback();
                System.Diagnostics.Debug.WriteLine(string.Format("Message:{0}\r\nStackTrace:{1}", 
                    ex.Message, ex.StackTrace), "Workflow Initialize");
                WorkflowLogger.Error(ex);
            }
            return isSuccess;
        }

        /// <summary>
        /// 解析并设置流程类型信息
        /// </summary>
        private void InitWorkflowInfo(XDocument wfXamlDoc, string workflowFullName, out int maxWfID)
        {
            string wfBussinessName = WorkflowXamlResolver.GetWorkflowBusinessName(wfXamlDoc);
            maxWfID = this.WorkflowInfoRepository.GetMaxWorkflowID();
            short maxVersion = this.WorkflowInfoRepository.GetMaxWorkflowVersion(workflowFullName);
            WorkflowInfo workflowInfo = new WorkflowInfo
            {
                ID = Guid.NewGuid().ToString(),
                WorkflowID = ++maxWfID,
                WorkflowFullName = workflowFullName,
                WorkflowBusinessName = wfBussinessName,
                Version = ++maxVersion,
                PublishTime = DateTime.Now
            };
            this.WorkflowInfoRepository.Create(workflowInfo);
        }

        /// <summary>
        /// 解析并设置环节信息
        /// </summary>
        private void InitActivities(XDocument wfXamlDoc, int maxWfID)
        {
            IEnumerable<XElement> activityElements = WorkflowXamlResolver.ParseActivities(wfXamlDoc);
            int maxActID = this.ActivityInfoRepository.GetMaxActivityID();
            foreach (var activity in activityElements)
            {
                ActivityInfo actInfo = new ActivityInfo
                {
                    ID = Guid.NewGuid().ToString(),
                    ActivityID = ++maxActID,
                    ActivityType = WorkflowXamlResolver.ParseActivityType(activity),
                    ActivityState = WorkflowXamlResolver.ParseActivityState(activity),
                    ActivityName = WorkflowXamlResolver.ParseActivityName(activity),
                    WorkflowID = maxWfID
                };
                this.ActivityInfoRepository.Create(actInfo);
            }
        }
        #endregion 初始化

        /// <summary>
        /// 获取流程类型列表
        /// </summary>
        public List<WorkflowInfo> GetWorkflowInfoList()
        {
            try
            {
                IEnumerable<WorkflowInfo> workflowInfoList = this.WorkflowInfoRepository.GetAll();
                return workflowInfoList.ToList();
            }
            catch (Exception ex)
            {
                WorkflowLogger.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 获取指定流程类型下的环节列表
        /// </summary>
        /// <param name="workflowID">流程类型编号</param>
        public List<ActivityInfo> GetActivities(int workflowID)
        {
            try
            {
                if (!int.TryParse(workflowID.ToString(), out workflowID))
                {
                    WorkflowLogger.Info("WorkflowConfigManager:Invalid format of the workflowID!");
                    return null;
                }
                return this.ActivityInfoRepository.GetActivities(workflowID).ToList();
            }
            catch (Exception ex)
            {
                WorkflowLogger.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 获取流程环节的处理人配置信息
        /// </summary>
        /// <param name="activityID">流程环节编号</param>
        public string GetActivityUser(int activityID)
        {
            try
            {
                ActivityInfo activity = this.ActivityInfoRepository.GetByActivityID(activityID);
                string operatorGroup = null == activity ? "" : activity.OperatorGroup;
                return operatorGroup;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Message:{0}\r\nStackTrace:{1}",
                    ex.Message, ex.StackTrace), "WorkflowConfig GetActivityOperatorGroup");
                WorkflowLogger.Error(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 保存流程环节的处理人配置信息
        /// </summary>
        /// <param name="id">流程环节信息ID（主键）</param>
        /// <param name="operatorGroup">处理人群组(角色ID集)，多个RoleID以"RoleID1,RoleID2,RoleID3..."格式传递</param>
        public void SaveActivityUser(string id, string operatorGroup)
        {
            try
            {
                ActivityInfo activity = this.ActivityInfoRepository.Get(id);
                activity.OperatorGroup = operatorGroup;
                this.ActivityInfoRepository.Update(activity);
                this.ActivityInfoRepository.Commit();
            }
            catch (Exception ex)
            {
                this.ActivityInfoRepository.Rollback();
                WorkflowLogger.Error(ex);
            }
        }
        #endregion 配置管理
    }
}
