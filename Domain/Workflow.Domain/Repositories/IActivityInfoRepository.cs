/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   IActivityInfoRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   规范流程环节信息仓储接口的行为
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

namespace Workflow.Domain.Repositories
{
    public interface IActivityInfoRepository : IRepository<ActivityInfo>
    {
        /// <summary>
        /// 根据环节编号获取环节信息
        /// </summary>
        /// <param name="activityID">环节编号</param>
        ActivityInfo GetByActivityID(int activityID);

        /// <summary>
        /// 根据流程类型编号获取流程环节列表
        /// </summary>
        /// <param name="workflowID">流程类型编号</param>
        IQueryable<ActivityInfo> GetActivities(int workflowID);

        /// <summary>
        /// 根据环节状态值和流程类型编号获取流程环节
        /// </summary>
        /// <param name="activityState">环节状态值</param>
        /// <param name="workflowID">流程类型编号</param>
        ActivityInfo GetActivity(string activityState, int workflowID);

        /// <summary>
        /// 获取当前流程环节库中最大的环节编号
        /// </summary>
        int GetMaxActivityID();
    }
}
