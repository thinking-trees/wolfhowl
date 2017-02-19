/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   HastenActivity.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-18
 *   职    责   ：   催单活动，为PerformActivity提供催单服务
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-05-18        1.0.0.0        余树杰        初版　 
 *   2015-09-10        1.1.0.0        余树杰        移除InstanceName在InitializeActivity创建后可再更改的功能
 *   2015-09-10        1.2.0.0        余树杰        修改书签名称为InstanceId，而非InstanceName
 *
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Activities;
using System.Text;
using Workflow.Activities.Properties;
using Workflow.Extensions;
using Workflow.Domain.Repositories;
using Workflow.Domain.DomainObjects;
using Workflow.Platform.Common.Interface;
using Workflow.Platform.Common.Entities;

namespace Workflow.Activities.CustomActivities
{
    /// <summary>
    /// 催单活动，为PerformActivity提供催单服务
    /// </summary>
    public sealed class HastenActivity : NativeActivity
    {
        #region 参数
        /// <summary>
        /// 标识环节类型
        /// </summary>
        private const string ACTIVITY_TYPE = "Hasten";
        private InArgument<string> _activityType = new InArgument<string>(ACTIVITY_TYPE);
        public InArgument<string> ActivityType
        {
            get
            {
                return this._activityType;
            }
            set
            {
                this._activityType = new InArgument<string>(ACTIVITY_TYPE);
            }
        }

        /// <summary>
        /// 当前环节ID
        /// </summary>
        public OutArgument<int> CurrentActivityID { get; set; }

        /// <summary>
        /// 当前环节处理人ID
        /// </summary>
        public OutArgument<string> CurrentUserID { get; set; }

        /// <summary>
        /// 当前环节处理人姓名
        /// </summary>
        public OutArgument<string> CurrentUserName { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public OutArgument<string> Comment { get; set; }

        /// <summary>
        /// 催单级别触发值，流程根据此值流转
        /// </summary>
        public OutArgument<string> HastenLevelTriggerValue { get; set; }

        /// <summary>
        /// 用户的操作命令
        /// </summary>
        public OutArgument<string> FlowOperation { get; set; }

        /// <summary>
        /// 用户的第二个操作命令
        /// </summary>
        //public OutArgument<string> SecondFlowOperation { get; set; }

        /// <summary>
        /// 用户的第三个操作命令
        /// 为了满足多条件判断的需求，后续增加此参数及功能
        /// 设计系统最多满足三级判断 
        /// </summary>
        //public OutArgument<string> ThirdFlowOperation { get; set; }

        /// <summary>
        /// 定义是否记录催单操作日志
        /// 默认为true
        /// </summary>
        private InArgument<bool> _isLogging = new InArgument<bool>(true);
        public InArgument<bool> IsLogging
        {
            get
            {
                return this._isLogging;
            }
            set
            {
                this._isLogging = value;
            }
        }

        /// <summary>
        /// 当前环节的状态值，在流程中必须唯一
        /// </summary>
        public InArgument<string> State { get; set; }
        #endregion 参数

        #region 属性
        /// <summary>
        /// 设置该活动能使工作流进入空闲状态
        /// </summary>
        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }
        #endregion 属性

        #region 环节处理
        protected override void Execute(NativeActivityContext context)
        {
            SaveRunningInstanceData workflowData = context.GetExtension<SaveRunningInstanceData>();
            IHastenService hastenSvc = context.GetExtension<IHastenService>();
            IActivityInfoRepository actInfoRepository = context.GetExtension<IActivityInfoRepository>();
            ActivityInfo hastenAct = actInfoRepository.GetActivity(this.State.Get(context), workflowData.WorkflowID);
            HastenServiceParams svcParams = new HastenServiceParams
            {
                InstanceID = context.WorkflowInstanceId.ToString(),
                InstanceName = workflowData.InstanceName,
                HastenActivityID = hastenAct.ActivityID,
                PerformActivityID = workflowData.CurrentActivityID,
                PerformActivityArrivalTime = workflowData.ArrivalTime,
                HastenAmount = workflowData.CurrentActivityHastenAmount
            };
            hastenSvc.Execute(svcParams);
            //创建书签并阻止当前实例向下执行
            context.CreateBookmark(context.WorkflowInstanceId.ToString(), OnResumeBookmark);            
        }
        #endregion 环节处理

        #region 方法
        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddDefaultExtensionProvider(() => new SaveRunningInstanceData());
        }

        /// <summary>
        /// 恢复书签，根据触发值进行流程流转
        /// </summary>
        private void OnResumeBookmark(NativeActivityContext context, Bookmark bookMark, object data)
        {
            var dataDictionary = (Dictionary<string, object>)data;
            SaveRunningInstanceData workflowData = context.GetExtension<SaveRunningInstanceData>();
            IWorkflowInstanceLogRepository logRepository = context.GetExtension<IWorkflowInstanceLogRepository>();

            if (!dataDictionary.ContainsKey("hastenLevelTriggerValue") || null == dataDictionary["hastenLevelTriggerValue"])
            {//恢复执行环节向下执行
                int nextActivityID = 0;//下一环节编号
                string nextUserID = null == dataDictionary["nextUserID"] ? "" : dataDictionary["nextUserID"].ToString();//下一环节处理人ID
                string NextUserName = null == dataDictionary["nextUserName"] ? "" : dataDictionary["nextUserName"].ToString();//下一环节处理人姓名
                if (!int.TryParse(dataDictionary["nextActivityID"].ToString(), out nextActivityID))
                {
                    throw new InvalidWorkflowException("Invalid format of the nextActivityID.");
                }
                this.CurrentActivityID.Set(context, nextActivityID);
                this.CurrentUserID.Set(context, nextUserID);
                this.CurrentUserName.Set(context, NextUserName);
                this.Comment.Set(context, dataDictionary["comment"].ToString());
                this.FlowOperation.Set(context, null == dataDictionary["flowOperation"] ? "" : dataDictionary["flowOperation"].ToString());//用户操作指示，可为null
                this.HastenLevelTriggerValue.Set(context, "-1000000000");
                workflowData.CurrentActivityHastenAmount = 0;//当前环节的催单次数重设为0

                //工作流实例操作日志记录
                WorkflowInstanceLog log = new WorkflowInstanceLog
                {
                    InstanceLogID = Guid.NewGuid().ToString(),
                    InstanceID = context.WorkflowInstanceId.ToString(),
                    ActivityName = workflowData.CurrentActivityName,
                    ActivityType = "Perform",
                    OperatorID = workflowData.CurrentUserID,//当前环节处理人ID,即上一环节传递的nextUserID
                    OperatorName = workflowData.CurrentUserName,//当前环节处理人姓名
                    FlowOperation = this.FlowOperation.Get(context),
                    Comment = this.Comment.Get(context),
                    FinishTime = DateTime.Now,
                    ArrivalTime = workflowData.ArrivalTime
                };
                logRepository.Create(log);
            }
            else
            {//催单
                this.HastenLevelTriggerValue.Set(context, dataDictionary["hastenLevelTriggerValue"].ToString());
                workflowData.CurrentActivityHastenAmount++;
                //记录催单历史，包含比催单日志更详细的信息
                //todo...
                bool isLogging = context.GetValue(this.IsLogging);
                if (isLogging)
                {//记录催单操作日志
                    WorkflowInstanceLog log = new WorkflowInstanceLog
                    {
                        InstanceLogID = Guid.NewGuid().ToString(),
                        InstanceID = context.WorkflowInstanceId.ToString(),
                        ActivityName = this.DisplayName,
                        ActivityType = ACTIVITY_TYPE,
                        OperatorID = workflowData.CurrentUserID,
                        OperatorName = workflowData.CurrentUserName,
                        Comment = string.Format(Resources.HastenComment, workflowData.CurrentActivityName, workflowData.CurrentActivityHastenAmount),
                        FinishTime = DateTime.Now,
                        ArrivalTime = workflowData.ArrivalTime
                    };
                    logRepository.Create(log);
                }
            }
        }
        #endregion 方法
    }
}
