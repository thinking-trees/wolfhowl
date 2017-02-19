/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   HastenTaskRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-18
 *   职    责   ：   实现催单任务仓储接口的行为
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-05-18        1.0.0.0        余树杰        初版　 
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
    public class HastenTaskRepository : Repository<HastenTask>, IHastenTaskRepository
    {
        public HastenTaskRepository(WorkflowDbContext unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// 检查流程实例是否已存在该环节的催单任务
        /// </summary>
        /// <param name="instanceID">流程实例ID</param>
        /// <param name="hastenActID">催单环节编号</param>
        /// <returns>存在:true;不存在:false</returns>
        public bool CheckHastenTask(string instanceID, int hastenActID)
        {
            var result = this.CurrentUnitOfWork.HastenTasks.Where(task => task.InstanceID.Equals(instanceID) && task.HastenActivityID.Equals(hastenActID));
            return result.Count() > 0;
        }
    }
}
