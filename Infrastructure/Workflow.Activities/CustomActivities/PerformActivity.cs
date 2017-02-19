/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   PerformActivity.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-09
 *   职    责   ：   可交互的执行活动（创建书签），等待用户响应操作后再往下执行，通用于流程的一般处理
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
using System.Text;
using Workflow.Extensions;
using Workflow.Domain.Repositories;
using Workflow.Domain.DomainObjects;
using Workflow.Platform.Common.Interface;
using Workflow.Platform.Common.Entities;

namespace Workflow.Activities.CustomActivities
{
    /// <summary>
    /// 可交互的执行活动（创建书签），等待用户响应操作后再往下执行，通用于流程的一般处理
    /// </summary>
    public sealed class PerformActivity : NativeActivityBase
    {
        #region 参数
        /// <summary>
        /// 标识环节类型
        /// </summary>
        private const string ACTIVITY_TYPE = "Perform";
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
        /// 定义此环节在空闲时是否需要实例数据持久化
        /// 默认为true
        /// </summary>
        private InArgument<bool> _isPersistence = new InArgument<bool>(true);
        public InArgument<bool> IsPersistence
        {
            get
            {
                return this._isPersistence;
            }
            set
            {
                this._isPersistence = value;
            }
        }

        /// <summary>
        /// 定义此环节是否可恢复，即是否在此活动创建书签
        /// 默认为true
        /// </summary>
        private InArgument<bool> _isResume = new InArgument<bool>(true);
        public InArgument<bool> IsResume
        {
            get
            {
                return this._isResume;
            }
            set
            {
                this._isResume = value;
            }
        }
        #endregion 参数

        #region 环节处理
        protected override void Execute(NativeActivityContext context)
        {
            bool isPersistence = context.GetValue(this.IsPersistence);
            bool isResume = context.GetValue(this.IsResume);
            if (isPersistence)
            {
                string currentUserID = context.GetValue(this.CurrentUserID);
                string currentUserName = context.GetValue(this.CurrentUserName);
                int currentActivityID = context.GetValue(this.CurrentActivityID);

                if (string.IsNullOrEmpty(currentUserID))
                {//若传递的NextUserID参数为空，则根据ActivityID获取已配置的处理人；若没有配置处理人，则返回错误信息并停止执行流程
                    IUser userProvider = context.GetExtension<IUser>();
                    IActivityInfoRepository activityRepository = context.GetExtension<IActivityInfoRepository>();
                    GetActivityUser(activityRepository, userProvider, currentActivityID, out currentUserID, out currentUserName);
                }

                #region 工作流实例数据持久化
                var updateWorkflowData = context.GetExtension<SaveRunningInstanceData>();
                updateWorkflowData.CurrentActivityName = this.DisplayName;
                updateWorkflowData.CurrentActivityID = currentActivityID;
                updateWorkflowData.CurrentUserID = currentUserID;
                updateWorkflowData.CurrentUserName = currentUserName;
                updateWorkflowData.ArrivalTime = DateTime.Now;
                updateWorkflowData.Comment = context.GetValue(this.Comment);
                string businessInfo = context.GetValue(this.BasicBusinessInfo);
                if (!string.IsNullOrEmpty(businessInfo))
                {
                    updateWorkflowData.BasicBusinessInfo = businessInfo;
                }
                #endregion 工作流实例数据持久化
            }
            if (isResume)
            {
                //创建书签并阻止当前实例向下执行
                context.CreateBookmark(context.WorkflowInstanceId.ToString(), OnResumePerformBookmark);
            }
        }

        /// <summary>
        /// 获取当前环节的处理人群组
        /// </summary>
        /// <param name="activityID">流程环节编号</param>
        private void GetActivityUser(IActivityInfoRepository activityRepository, IUser userProvider, int activityID, out string userID, out string userName)
        {
            try
            {
                userID = "";
                userName = "";
                ActivityInfo activity = activityRepository.GetByActivityID(activityID);
                if (!activity.ActivityName.Equals("End") && null != userProvider)
                {//非流程结束环节时执行
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
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion 环节处理
    }
}
