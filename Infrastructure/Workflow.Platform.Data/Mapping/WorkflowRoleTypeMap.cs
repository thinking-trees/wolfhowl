/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowRoleTypeMap.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   配置WorkflowRole类与数据库的映射关系
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
using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.DomainObjects;

namespace Workflow.Platform.Data.Mapping
{
    /// <summary>
    /// 流程角色/用户信息映射配置类
    /// </summary>
    public class WorkflowRoleTypeMap : EntityTypeConfiguration<WorkflowRole>
    {
        public WorkflowRoleTypeMap(string dbSchema)
        {
            this.ToTable("WorkflowRoles", dbSchema);
            this.HasKey(role => role.ID);
            this.Property(role => role.ID).HasColumnName("WorkflowRoleID");
            this.Property(role => role.Type).HasColumnName("Type");
            this.Property(role => role.Code).HasColumnName("Code");
            this.Property(role => role.Name).HasColumnName("Name");
            this.Property(role => role.Department).HasColumnName("Department");
            this.Property(role => role.ParentID).HasColumnName("ParentID");
            this.Property(role => role.CreateTime).HasColumnName("CreateTime");
        }
    }
}
