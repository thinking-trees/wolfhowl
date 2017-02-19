/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowRole.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   流程角色/用户信息
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

namespace Workflow.Domain.DomainObjects
{
    /// <summary>
    /// 流程角色/用户信息
    /// </summary>
    public class WorkflowRole
    {
        #region 属性
        /// <summary>
        /// 角色/用户ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 类型；Role：角色，User：用户
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 角色/用户编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 角色/用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色/用户部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 父角色/用户ID
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        #endregion 属性
    }
}
