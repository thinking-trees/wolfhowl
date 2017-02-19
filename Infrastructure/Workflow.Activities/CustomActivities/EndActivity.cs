/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   EndActivity.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-26
 *   职    责   ：   结束活动，停止流程的执行并保存流程日志及当前状态
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-26        1.0.0.0        余树杰        初版　 
 *    
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Activities;
using Workflow.Activities.Properties;
using Workflow.Domain.DomainObjects;
using Workflow.Domain.Repositories;
using System.Activities.Statements;

namespace Workflow.Activities.CustomActivities
{
    /// <summary>
    /// 结束活动，停止流程的执行并保存流程日志及当前状态
    /// </summary>
    public sealed class EndActivity : NativeActivity
    {
        #region 参数
        /// <summary>
        /// 标识环节类型
        /// </summary>
        private const string ACTIVITY_TYPE = "End";
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
        /// 当前环节的状态值，在流程中必须唯一
        /// </summary>
        private InArgument<string> _state = new InArgument<string>("End");
        public InArgument<string> State
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = new InArgument<string>("End");
            }
        }
        #endregion 参数

        #region Metadata
        /// <summary>
        /// TerminateWorkflow活动，用于结束工作流实例
        /// </summary>
        private Activity _terminateActivity = new TerminateWorkflow { Reason = Resources.TerminateWorkflowReason };

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            //将TerminateWorkflow添加到EndActivity活动的子活动元数据列表中
            //若不通过AddChild方法添加到子活动元数据列表，直接以NativeActivityContext.ScheduleActivity的方式执行TerminateWorkflow，运行时会报错
            metadata.AddChild(this._terminateActivity);
        }
        #endregion Metadata

        #region 环节处理
        protected override void Execute(NativeActivityContext context)
        {
            //工作流实例操作日志记录
            IWorkflowInstanceLogRepository logRepository = context.GetExtension<IWorkflowInstanceLogRepository>();
            WorkflowInstanceLog log = new WorkflowInstanceLog
            {
                InstanceLogID = Guid.NewGuid().ToString(),
                InstanceID = context.WorkflowInstanceId.ToString(),
                ActivityName = this.DisplayName,
                ActivityType = ACTIVITY_TYPE,
                OperatorID = string.Empty,
                OperatorName = string.Empty,
                FinishTime = DateTime.Now,
                Comment = ACTIVITY_TYPE
            };
            logRepository.Create(log);

            //停止流程的执行，以子活动的方式执行TerminateWorkflow活动
            context.ScheduleActivity(this._terminateActivity);
        }
        #endregion 环节处理
    }
}
