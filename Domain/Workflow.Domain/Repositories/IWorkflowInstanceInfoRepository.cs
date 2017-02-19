/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   IWorkflowInstanceInfoRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-18
 *   职    责   ：   规范工作流实例基本信息仓储接口的行为
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
using Workflow.Domain.DomainObjects;

namespace Workflow.Domain.Repositories
{
    public interface IWorkflowInstanceInfoRepository : IRepository<WorkflowInstanceInfo>
    {
        /// <summary>
        /// 根据流程实例ID获取实例信息
        /// </summary>
        /// <param name="instanceID">流程实例ID</param>
        WorkflowInstanceInfo GetByInstanceID(string instanceID);
    }
}
