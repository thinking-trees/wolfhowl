/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   EmailActivity.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-06-02
 *   职    责   ：   邮件通知活动
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-06-02        1.0.0.0        余树杰        初版　 
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

    public sealed class EmailActivity : CodeActivity
    {
        #region 参数
        /// <summary>
        /// 标识环节类型
        /// </summary>
        private const string ACTIVITY_TYPE = "EmailNotify";
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

        #region 方法
        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddDefaultExtensionProvider(() => new SaveRunningInstanceData());
        }
        #endregion 方法

        #region 环节处理
        protected override void Execute(CodeActivityContext context)
        {
            bool isLogging = context.GetValue(this.IsLogging);
            var workflowData = context.GetExtension<SaveRunningInstanceData>();
            //根据该环节的配置获取邮件接收人、邮件内容等，然后发送通知
            IActivityInfoRepository actInfoRepository = context.GetExtension<IActivityInfoRepository>();
            ActivityInfo performAct = actInfoRepository.GetByActivityID(workflowData.CurrentActivityID);
            ActivityInfo emailAct = actInfoRepository.GetActivity(this.State.Get(context), performAct.WorkflowID);
            //todo...

            if (isLogging)
            {//工作流实例操作日志记录
                IWorkflowInstanceLogRepository logRepository = context.GetExtension<IWorkflowInstanceLogRepository>();
                WorkflowInstanceLog log = new WorkflowInstanceLog
                {
                    InstanceLogID = Guid.NewGuid().ToString(),
                    InstanceID = context.WorkflowInstanceId.ToString(),
                    ActivityName = this.DisplayName,
                    ActivityType = ACTIVITY_TYPE,
                    OperatorID = string.Empty,
                    OperatorName = string.Empty,
                    Comment = ACTIVITY_TYPE,
                    ArrivalTime = DateTime.Now,
                    FinishTime = DateTime.Now
                };
                logRepository.Create(log);
            }            
        }
        #endregion 环节处理
    }
}
