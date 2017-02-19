﻿/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowInstanceLogRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   实现流程实例日志仓储接口的行为
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
    public class WorkflowInstanceLogRepository : Repository<WorkflowInstanceLog>, IWorkflowInstanceLogRepository
    {
        public WorkflowInstanceLogRepository(WorkflowDbContext unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// 根据流程实例ID获取该实例的所有流程日志
        /// </summary>
        /// <param name="instanceID">流程实例ID</param>
        public IQueryable<WorkflowInstanceLog> GetWorkflowInstanceLogs(string instanceID)
        {
            return this.CurrentUnitOfWork.WorkflowInstanceLogs.Where(log => log.InstanceID.Equals(instanceID)).OrderBy(l => l.FinishTime);
        }
    }
}
