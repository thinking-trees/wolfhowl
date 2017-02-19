/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   IWorkflowRoleRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   规范流程角色/用户信息仓储接口的行为
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
    public interface IWorkflowRoleRepository : IRepository<WorkflowRole>
    {
        /// <summary>
        /// 根据类型获取角色/用户信息
        /// </summary>
        /// <param name="type">类型；Role：角色，User：用户</param>
        IQueryable<WorkflowRole> GetWorkflowRoles(string type);

        /// <summary>
        /// 根据ID获取角色/用户信息
        /// </summary>
        /// <param name="ids">角色/用户人ID数组</param>
        IQueryable<WorkflowRole> GetWorkflowRoles(string[] ids);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="roleId">角色/用户的ID</param>
        IQueryable<WorkflowRole> GetUsersByID(string id);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="code">角色/用户编号</param>
        IQueryable<WorkflowRole> GetUsersByCode(string code);

        /// <summary>
        /// 检查同一类型数据的编号是否存在
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="type">类型</param>
        bool CheckCodeExistsByType(string code, string type);
    }
}
