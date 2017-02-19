/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   ProcessedInstanceRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-16
 *   职    责   ：   实现已办流程任务仓储接口的行为
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-16        1.0.0.0        余树杰        初版　 
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
    public class ProcessedInstanceRepository : Repository<ProcessedInstance>, IProcessedInstanceRepository
    {
        public ProcessedInstanceRepository(WorkflowDbContext unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// 获取已办流程任务列表
        /// </summary>
        /// <param name="userID">处理人ID</param>
        public IQueryable<ProcessedInstance> GetProcessedInstances(string userID)
        {
            string sql = "select * from [EMERSON].[ProcessedInstances]";
            var query = CurrentUnitOfWork.Database.SqlQuery<ProcessedInstance>(sql)
                        .Where(p => p.OperatorID.Contains(userID))
                        .OrderByDescending(p => p.CreationTime);
            return query.AsQueryable();
        }
    }
}
