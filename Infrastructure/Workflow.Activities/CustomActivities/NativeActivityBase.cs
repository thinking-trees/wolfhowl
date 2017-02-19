/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   NativeActivityBase.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-04-21
 *   职    责   ：   提供可与工作流运行时交互的基础活动
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-04-21        1.0.0.0        余树杰        初版　 
 *   2015-05-20        1.1.0.0        余树杰        增加当前环节的状态值State，可根据此值与WorkflowID获取ActivityID
 *   2015-09-10        1.2.0.0        余树杰        移除InstanceName在InitializeActivity创建后可再更改的功能
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
    /// 提供可交互的基础活动
    /// </summary>
    public abstract class NativeActivityBase : NativeActivity
    {
        #region 参数
        /// <summary>
        /// 当前环节ID
        /// </summary>
        public InOutArgument<int> CurrentActivityID { get; set; }

        /// <summary>
        /// 当前环节处理人ID
        /// </summary>
        public InOutArgument<string> CurrentUserID { get; set; }

        /// <summary>
        /// 当前环节处理人姓名
        /// </summary>
        public InOutArgument<string> CurrentUserName { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public InOutArgument<string> Comment { get; set; }

        /// <summary>
        /// 流程的基础业务信息
        /// </summary>
        public InOutArgument<string> BasicBusinessInfo { get; set; }

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

        #region 方法
        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddDefaultExtensionProvider(() => new SaveRunningInstanceData());
        }

        /// <summary>
        /// 恢复书签时，将用户的操作命令传给正在执行的工作流实例
        /// </summary>
        protected void OnResumePerformBookmark(NativeActivityContext context, Bookmark bookMark, object data)
        {
            //设置活动执行所需参数
            var dataDictionary = (Dictionary<string, object>)data;
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
            this.BasicBusinessInfo.Set(context, dataDictionary["basicBusinessInfo"] == null ? "" : dataDictionary["basicBusinessInfo"].ToString());
            this.FlowOperation.Set(context, dataDictionary["flowOperation"] == null ? "" : dataDictionary["flowOperation"].ToString());//用户操作指示，可为null

            //工作流实例操作日志记录
            var updateWorkflowData = context.GetExtension<SaveRunningInstanceData>();
            IWorkflowInstanceLogRepository logRepository = context.GetExtension<IWorkflowInstanceLogRepository>();
            WorkflowInstanceLog log = new WorkflowInstanceLog
            {
                InstanceLogID = Guid.NewGuid().ToString(),
                InstanceID = context.WorkflowInstanceId.ToString(),
                ActivityName = this.DisplayName,
                ActivityType = "Perform",
                OperatorID = updateWorkflowData.CurrentUserID,//当前环节处理人ID,即上一环节传递的nextUserID
                OperatorName = updateWorkflowData.CurrentUserName,//当前环节处理人姓名
                FlowOperation = this.FlowOperation.Get(context),
                Comment = this.Comment.Get(context),
                ArrivalTime = updateWorkflowData.ArrivalTime,
                FinishTime = DateTime.Now
            };
            logRepository.Create(log);
        }
        #endregion 方法
    }
}
