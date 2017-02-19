/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowInstanceInfoTypeMap.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-18
 *   职    责   ：   配置WorkflowInstanceInfo类与数据库的映射关系
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
    /// <summary>
    /// 工作流实例基本信息映射配置类
    /// </summary>
    public class WorkflowInstanceInfoTypeMap : EntityTypeConfiguration<WorkflowInstanceInfo>
    {
        public WorkflowInstanceInfoTypeMap(string schema)
        {
            this.ToTable("InstancePromotedPropertiesTable", "System.Activities.DurableInstancing");
            this.HasKey(info => info.InstanceID);
            this.Property(info => info.InstanceID).HasColumnName("Value11");
            this.Property(info => info.InstanceName).HasColumnName("Value1");
            this.Property(info => info.InstanceType).HasColumnName("PromotionName");
            this.Property(info => info.CreateUserID).HasColumnName("Value2");
            this.Property(info => info.CreateUserName).HasColumnName("Value3");
            this.Property(info => info.CreationTime).HasColumnName("Value4");
            this.Property(info => info.CurrentUserID).HasColumnName("Value7");
            this.Property(info => info.CurrentUserName).HasColumnName("Value8");
            this.Property(info => info.CurrentActivityID).HasColumnName("Value5");
            this.Property(info => info.CurrentActivityName).HasColumnName("Value6");
            this.Property(info => info.CurrentActivityArrivalTime).HasColumnName("Value9");
            this.Property(info => info.Comment).HasColumnName("Value10");
            this.Property(info => info.WorkflowID).HasColumnName("Value12");
            this.Property(info => info.BasicBusinessInfo).HasColumnName("Value13");
            this.Property(info => info.CurrentActivityHastenAmount).HasColumnName("Value14");
        }
    }
}
