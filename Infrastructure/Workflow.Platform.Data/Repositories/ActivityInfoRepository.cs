/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   ActivityInfoRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   实现流程环节信息仓储接口的行为
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-17        1.0.0.0        余树杰        初版　 
 *    
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.Domain.DomainObjects;
using Workflow.Domain.Repositories;
using Workflow.Platform.Data.UnitOfWork;

namespace Workflow.Platform.Data.Repositories
{
    public class ActivityInfoRepository : Repository<ActivityInfo>, IActivityInfoRepository
    {
        public ActivityInfoRepository(WorkflowDbContext unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// 根据环节编号获取环节信息
        /// </summary>
        /// <param name="activityID">环节编号</param>
        public ActivityInfo GetByActivityID(int activityID)
        {
            try
            {
                return this.CurrentUnitOfWork.ActivityInfos.SingleOrDefault(info => info.ActivityID.Equals(activityID));
            }
            catch
            {
                throw new Exception("The format of activityID is error or exists the entity more than one");
            }
        }

        /// <summary>
        /// 根据流程类型编号获取流程环节列表
        /// </summary>
        /// <param name="workflowID">流程类型编号</param>
        public IQueryable<ActivityInfo> GetActivities(int workflowID)
        {
            return this.CurrentUnitOfWork.ActivityInfos.Where(info => info.WorkflowID.Equals(workflowID));
        }

        /// <summary>
        /// 根据环节状态值和流程类型编号获取流程环节
        /// </summary>
        /// <param name="activityState">环节状态值</param>
        /// <param name="workflowID">流程类型编号</param>
        public ActivityInfo GetActivity(string activityState, int workflowID)
        {
            return this.CurrentUnitOfWork.ActivityInfos.SingleOrDefault(info => info.ActivityState.Equals(activityState) && info.WorkflowID.Equals(workflowID));
        }

        /// <summary>
        /// 获取当前流程环节库中最大的环节编号
        /// </summary>
        public int GetMaxActivityID()
        {
            int maxActID = 0;
            int actCount = this.CurrentUnitOfWork.ActivityInfos.Count();
            if (actCount > 0)
            {
                maxActID = this.CurrentUnitOfWork.ActivityInfos.Max(info => info.ActivityID);
            }
            return maxActID;
        }
    }
}
