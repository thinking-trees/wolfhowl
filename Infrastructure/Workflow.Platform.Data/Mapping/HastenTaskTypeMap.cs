/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   HastenTaskTypeMap.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-18
 *   职    责   ：   配置HastenTask类与数据库的映射关系
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
using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.DomainObjects;

namespace Workflow.Platform.Data.Mapping
{
    public class HastenTaskTypeMap : EntityTypeConfiguration<HastenTask>
    {
        public HastenTaskTypeMap(string schema)
        {
            this.ToTable("HastenTasks", schema);
            this.HasKey(task => task.HastenTaskID);
            this.Property(task => task.HastenTaskID).HasColumnName("HastenTaskID");
            this.Property(task => task.InstanceID).HasColumnName("InstanceID");
            this.Property(task => task.InstanceName).HasColumnName("InstanceName");
            this.Property(task => task.PerformActivityID).HasColumnName("PerformActivityID");
            this.Property(task => task.PerformActivityArrivalTime).HasColumnName("PerformActivityArrivalTime");
            this.Property(task => task.HastenActivityID).HasColumnName("HastenActivityID");
            this.Property(task => task.HastenAmount).HasColumnName("HastenAmount");
        }
    }
}
