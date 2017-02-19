/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   ForwardActivity.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-21
 *   职    责   ：   流程转发，除了更改当前处理人和接收时间，其它基本信息不变
 *                   
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-05-21        1.0.0.0        余树杰        初版　 
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
    /// 流程转发活动
    /// </summary>
    public sealed class ForwardActivity : CodeActivity
    {
        #region 参数
        /// <summary>
        /// 标识环节类型
        /// </summary>
        private const string ACTIVITY_TYPE = "Forward";
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
        /// 禁用下一个活动更新实例持久化数据的功能
        /// </summary>
        public OutArgument<bool> DisablePersistenceOnNext { get; set; }

        /// <summary>
        /// 定义此环节是否记录操作日志
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
            string logComment = "";
            var workflowData = context.GetExtension<SaveRunningInstanceData>();
            string preUserID = workflowData.CurrentUserID;//上一环节处理人ID,需要记录日志时使用
            string preUserName = workflowData.CurrentUserName;
            string currentUserID = context.GetValue(this.CurrentUserID);
            string currentUserName = context.GetValue(this.CurrentUserName);
            IActivityInfoRepository actInfoRepository = context.GetExtension<IActivityInfoRepository>();
            ActivityInfo performAct = actInfoRepository.GetByActivityID(workflowData.CurrentActivityID);
            ActivityInfo forwardAct = actInfoRepository.GetActivity(this.State.Get(context), performAct.WorkflowID);

            if (string.IsNullOrEmpty(currentUserID))
            {//若传递的NextUserID参数为空，则根据ActivityID获取已配置的处理人
                IUser userProvider = context.GetExtension<IUser>();
                var dict = GetActivityUser(actInfoRepository, userProvider, forwardAct.ActivityID);
                currentUserID = dict["userID"] ?? preUserID;
                currentUserName = dict["userName"] ?? preUserName;
                logComment = dict["logComment"];
            }

            //工作流实例数据持久化
            workflowData.CurrentUserID = currentUserID;
            workflowData.CurrentUserName = currentUserName;
            workflowData.ArrivalTime = DateTime.Now;
            workflowData.Comment = context.GetValue(this.Comment) ?? workflowData.Comment;

            if (isLogging)
            {//工作流实例操作日志记录
                IWorkflowInstanceLogRepository logRepository = context.GetExtension<IWorkflowInstanceLogRepository>();
                WorkflowInstanceLog log = new WorkflowInstanceLog
                {
                    InstanceLogID = Guid.NewGuid().ToString(),
                    InstanceID = context.WorkflowInstanceId.ToString(),
                    ActivityName = this.DisplayName,
                    ActivityType = ACTIVITY_TYPE,
                    OperatorID = preUserID,
                    OperatorName = preUserName,
                    Comment = logComment,
                    ArrivalTime = workflowData.ArrivalTime,
                    FinishTime = DateTime.Now
                };
                logRepository.Create(log);
            }

            //禁用下一个活动更新实例持久化数据的功能
            this.DisablePersistenceOnNext.Set(context, false);
        }

        /// <summary>
        /// 获取当前环节的处理人群组
        /// </summary>
        /// <returns></returns>
        private Dictionary<string,string> GetActivityUser(IActivityInfoRepository actInfoRepository, IUser userProvider, int activityID)
        {
            string userID = null;
            string userName = null;
            string logComment = Resources.ForwardSuccess;
            try
            {
                ActivityInfo activity = actInfoRepository.GetByActivityID(activityID);
                string operatorGroup = activity.OperatorGroup;
                List<UserInfo> user = userProvider.GetUserByRoleID(operatorGroup);
                StringBuilder userIDBuilder = new StringBuilder();
                StringBuilder userNameBuilder = new StringBuilder();
                user.ForEach(u =>
                {
                    userIDBuilder.Append(string.Format("{0},", u.UserID));
                    userNameBuilder.Append(string.Format("{0},", u.UserName));
                });
                userID = userIDBuilder.ToString().Remove(userIDBuilder.ToString().LastIndexOf(','));
                userName = userNameBuilder.ToString().Remove(userNameBuilder.ToString().LastIndexOf(','));
            }
            catch
            {
                logComment = Resources.ForwardFail;
                throw;
            }
            return new Dictionary<string, string>
            {
                { "userID", userID },
                { "userName", userName },
                { "logComment", logComment }
            };
        }
        #endregion 环节处理
    }
}
