/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowInfoRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   实现流程类型信息仓储接口的行为
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
    public class WorkflowInfoRepository : Repository<WorkflowInfo>, IWorkflowInfoRepository
    {
        public WorkflowInfoRepository(WorkflowDbContext unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// 根据流程类型编号获取流程类型信息
        /// </summary>
        /// <param name="workflowID">流程类型编号</param>
        public WorkflowInfo GetByWorkflowID(int workflowID)
        {
            try
            {
                return this.CurrentUnitOfWork.WorkflowInfos.SingleOrDefault(info => info.WorkflowID.Equals(workflowID));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex, "WorkflowInfoRepository GetByWorkflowID");
                throw;
            }
        }

        /// <summary>
        /// 获取当前流程类型库中最大的编号
        /// 用于新流程类型初始化时设置流程类型编号
        /// </summary>
        public int GetMaxWorkflowID()
        {
            int maxWorkflowID = 0;
            int workflowCount = this.CurrentUnitOfWork.WorkflowInfos.Count();
            if (workflowCount > 0)
            {
                maxWorkflowID = this.CurrentUnitOfWork.WorkflowInfos.Max(info => info.WorkflowID);
            }
            return maxWorkflowID;
        }

        /// <summary>
        /// 根据流程类型的完全限定名称获取该流程类型的最大版本号
        /// </summary>
        /// <param name="workflowFullName">流程类型的完全限定名称</param>
        public short GetMaxWorkflowVersion(string workflowFullName)
        {
            IQueryable<WorkflowInfo> infos = this.CurrentUnitOfWork.WorkflowInfos.Where(info => info.WorkflowFullName.Equals(workflowFullName));
            short version = infos.Max(info => info.Version) ?? 0;
            return version;
        }
    }
}
