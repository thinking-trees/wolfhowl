/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowInstanceInfoRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-18
 *   职    责   ：   实现工作流实例基本信息仓储接口的行为
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
    public class WorkflowInstanceInfoRepository : Repository<WorkflowInstanceInfo>, IWorkflowInstanceInfoRepository
    {
        public WorkflowInstanceInfoRepository(WorkflowDbContext unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// 根据流程实例ID获取实例信息
        /// </summary>
        /// <param name="instanceID">流程实例ID</param>
        public WorkflowInstanceInfo GetByInstanceID(string instanceID)
        {
            try
            {
                return this.CurrentUnitOfWork.WorkflowInstanceInfos.SingleOrDefault(info => info.InstanceID.Equals(instanceID));
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex, "WorkflowInstanceInfoRepository GetByInstanceID");
                throw;
            }
        }
    }
}
