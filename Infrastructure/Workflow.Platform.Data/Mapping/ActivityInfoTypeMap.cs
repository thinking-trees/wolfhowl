/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   ActivityInfoTypeMap.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   配置ActivityInfo类与数据库的映射关系
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-17        1.0.0.0        余树杰        初版　 
 *   2015-05-18        1.1.0.0        余树杰        增加ActivityType字段的映射 
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
    /// 流程环节信息映射配置类
    /// </summary>
    public class ActivityInfoTypeMap : EntityTypeConfiguration<ActivityInfo>
    {
        public ActivityInfoTypeMap(string dbSchema)
        {
            this.ToTable("ActivityInfo", dbSchema);
            this.HasKey(info => info.ID);
            this.Property(info => info.ID).HasColumnName("ActivityInfoID");
            this.Property(info => info.ActivityID).HasColumnName("ActivityID");
            this.Property(info => info.ActivityType).HasColumnName("ActivityType");
            this.Property(info => info.ActivityState).HasColumnName("ActivityState");
            this.Property(info => info.ActivityName).HasColumnName("ActivityName");
            this.Property(info => info.OperatorGroup).HasColumnName("OperatorGroup");
            this.Property(info => info.WorkflowID).HasColumnName("WorkflowID");
        }
    }
}
