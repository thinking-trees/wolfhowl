/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   HastenHistoryTypeMap.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-18
 *   职    责   ：   配置HastenHistory类与数据库的映射关系
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
    public class HastenHistoryTypeMap : EntityTypeConfiguration<HastenHistory>
    {
        public HastenHistoryTypeMap(string schema)
        {
            this.ToTable("HastenHistoryList", schema);
            this.HasKey(h => h.HastenHistoryID);
            this.Property(h => h.HastenHistoryID).HasColumnName("HastenHistoryID");
            this.Property(h => h.InstanceID).HasColumnName("InstanceID");
            this.Property(h => h.PerformActivityID).HasColumnName("PerformActivityID");
            this.Property(h => h.PerformActivityName).HasColumnName("PerformActivityName");
            this.Property(h => h.HastenActivityID).HasColumnName("HastenActivityID");
            this.Property(h => h.HastenActivityName).HasColumnName("HastenActivityName");
            this.Property(h => h.HastenUserID).HasColumnName("HastenUserID");
            this.Property(h => h.HastenUserName).HasColumnName("HastenUserName");
            this.Property(h => h.TriggerTime).HasColumnName("TriggerTime");
            this.Property(h => h.HastenTime).HasColumnName("HastenTime");
            this.Property(h => h.HastenTriggerValue).HasColumnName("HastenTriggerValue");
        }
    }
}
