/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowInfoTypeMap.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   配置WorkflowInfo类与数据库的映射关系
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
    /// 流程类型信息映射配置类
    /// </summary>
    public class WorkflowInfoTypeMap : EntityTypeConfiguration<WorkflowInfo>
    {
        public WorkflowInfoTypeMap(string dbSchema)
        {
            this.ToTable("WorkflowInfo", dbSchema);
            this.HasKey(info => info.ID);
            this.Property(info => info.ID).HasColumnName("WorkflowInfoID");
            this.Property(info => info.WorkflowID).HasColumnName("WorkflowID");
            this.Property(info => info.WorkflowFullName).HasColumnName("WorkflowFullName");
            this.Property(info => info.WorkflowBusinessName).HasColumnName("WorkflowBusinessName");
            this.Property(info => info.Version).HasColumnName("Version");
            this.Property(info => info.PublishTime).HasColumnName("PublishTime");
        }
    }
}
