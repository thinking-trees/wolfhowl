/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   IWorkflowInfoRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   规范流程类型信息仓储接口的行为
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
    public interface IWorkflowInfoRepository : IRepository<WorkflowInfo>
    {
        /// <summary>
        /// 根据流程类型编号获取流程类型信息
        /// </summary>
        /// <param name="workflowID">流程类型编号</param>
        WorkflowInfo GetByWorkflowID(int workflowID);

        /// <summary>
        /// 获取当前流程类型库中最大的编号
        /// 用于新流程类型初始化时设置流程类型编号
        /// </summary>
        int GetMaxWorkflowID();

        /// <summary>
        /// 根据流程类型的完全限定名称获取该流程类型的最大版本号
        /// </summary>
        /// <param name="workflowFullName">流程类型的完全限定名称</param>
        short GetMaxWorkflowVersion(string workflowFullName);
    }
}
