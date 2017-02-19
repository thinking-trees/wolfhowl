/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   InitializeActivity.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-09
 *   职    责   ：   初始化活动，为流程提供初始化参数
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-09        1.0.0.0        余树杰        初版　 
 *   2015-04-21        2.0.0.0        余树杰        从NativeActivityBase类中继承基础参数及方法
 *   2015-09-10        2.1.0.0        余树杰        移除InstanceName在InitializeActivity创建后可再更改的功能
 *   2015-09-10        2.2.0.0        余树杰        修改书签名称为InstanceId，而非InstanceName
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Activities;
using Workflow.Extensions;
using Workflow.Domain.DomainObjects;
using Workflow.Domain.Repositories;

namespace Workflow.Activities.CustomActivities
{
    /// <summary>
    /// 初始化活动，为流程提供初始化参数
    /// </summary>
    public sealed class InitializeActivity : NativeActivityBase
    {
        #region 参数
        /// <summary>
        /// 标识环节类型
        /// </summary>
        private const string ACTIVITY_TYPE = "Initialize";
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
        /// 流程实例名称
        /// </summary>
        public InOutArgument<string> InstanceName { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        public InArgument<string> CreateUserID { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        public InArgument<string> CreateUserName { get; set; }
        #endregion 参数

        #region 环节处理
        protected override void Execute(NativeActivityContext context)
        {
            #region 工作流实例数据持久化
            var initialWorkflowData = context.GetExtension<SaveRunningInstanceData>();
            initialWorkflowData.CreateUserID = string.IsNullOrEmpty(initialWorkflowData.CreateUserID) ? context.GetValue(this.CreateUserID) : initialWorkflowData.CreateUserID;
            initialWorkflowData.CreateUserName = string.IsNullOrEmpty(initialWorkflowData.CreateUserName) ? context.GetValue(this.CreateUserName) : initialWorkflowData.CreateUserName;
            initialWorkflowData.CreationTime = DateTime.Now;
            initialWorkflowData.CurrentUserID = initialWorkflowData.CreateUserID;//InitialActivity的处理人为创建者
            initialWorkflowData.CurrentUserName = initialWorkflowData.CreateUserName;
            initialWorkflowData.Comment = context.GetValue(this.Comment);
            string businessInfo = context.GetValue(this.BasicBusinessInfo);
            if (!string.IsNullOrEmpty(businessInfo))
            {
                initialWorkflowData.BasicBusinessInfo = businessInfo;
            }
            #endregion 工作流实例数据持久化

            int preActivityID = initialWorkflowData.CurrentActivityID;//上一环节的编号
            int currentActivityID = context.GetValue(this.CurrentActivityID);//当前环节的编号
            if (0 != preActivityID && currentActivityID < preActivityID)
            {//流程实例退回时创建书签
                initialWorkflowData.CurrentActivityID = currentActivityID;
                initialWorkflowData.CurrentActivityName = this.DisplayName;
                context.CreateBookmark(context.WorkflowInstanceId.ToString(), OnResumePerformBookmark);
            }
            else
            {//流程实例新建时
                IActivityInfoRepository actInfoRepository = context.GetExtension<IActivityInfoRepository>();
                ActivityInfo actInfo = actInfoRepository.GetByActivityID(currentActivityID);
                initialWorkflowData.WorkflowID = actInfo.WorkflowID;
                initialWorkflowData.InstanceID = context.WorkflowInstanceId.ToString();
                initialWorkflowData.InstanceName = context.GetValue(this.InstanceName);
                initialWorkflowData.BasicBusinessInfo = businessInfo ?? string.Empty;
                IWorkflowInstanceLogRepository logRepository = context.GetExtension<IWorkflowInstanceLogRepository>();
                WorkflowInstanceLog log = new WorkflowInstanceLog
                {
                    InstanceLogID = Guid.NewGuid().ToString(),
                    InstanceID = context.WorkflowInstanceId.ToString(),
                    ActivityName = this.DisplayName,
                    ActivityType = ACTIVITY_TYPE,
                    OperatorID = context.GetValue(this.CreateUserID),
                    OperatorName = context.GetValue(this.CreateUserName),
                    FinishTime = DateTime.Now,
                    Comment = context.GetValue(this.Comment)
                };
                logRepository.Create(log);
            }
        }
        #endregion 环节处理
    }
}
