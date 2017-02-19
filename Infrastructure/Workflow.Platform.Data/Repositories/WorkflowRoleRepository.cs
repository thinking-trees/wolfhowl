/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowRoleRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   实现流程角色/用户信息仓储接口的行为
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
    public class WorkflowRoleRepository : Repository<WorkflowRole>, IWorkflowRoleRepository
    {
        public WorkflowRoleRepository(WorkflowDbContext unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// 根据类型获取角色/用户信息
        /// </summary>
        /// <param name="type">类型；Role：角色，User：用户</param>
        public IQueryable<WorkflowRole> GetWorkflowRoles(string type)
        {
            return null;
        }

        /// <summary>
        /// 根据ID获取角色/用户信息
        /// </summary>
        /// <param name="ids">角色/用户人ID数组</param>
        public IQueryable<WorkflowRole> GetWorkflowRoles(string[] ids)
        {
            return null;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="roleId">角色/用户的ID</param>
        public IQueryable<WorkflowRole> GetUsersByID(string id)
        {
            return null;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="code">角色/用户编号</param>
        public IQueryable<WorkflowRole> GetUsersByCode(string code)
        {
            return null;
        }

        /// <summary>
        /// 检查同一类型数据的编号是否存在
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="type">类型</param>
        public bool CheckCodeExistsByType(string code, string type)
        {
            return false;
        }
    }
}
