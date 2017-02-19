/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowInstanceLogTypeMap.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   配置WorkflowInstanceLog类与数据库的映射关系
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
    /// 流程实例日志映射配置类
    /// </summary>
    public class WorkflowInstanceLogTypeMap : EntityTypeConfiguration<WorkflowInstanceLog>
    {
        public WorkflowInstanceLogTypeMap(string dbSchema)
        {
            this.ToTable("WorkflowInstanceLogs", dbSchema);
            this.HasKey(log => log.InstanceLogID);
            this.Property(log => log.InstanceLogID).HasColumnName("InstanceLogID");
            this.Property(log => log.InstanceID).HasColumnName("InstanceID");
            this.Property(log => log.ActivityName).HasColumnName("ActivityName");
            this.Property(log => log.ActivityType).HasColumnName("ActivityType");
            this.Property(log => log.OperatorID).HasColumnName("OperatorID");
            this.Property(log => log.OperatorName).HasColumnName("OperatorName");
            this.Property(log => log.FlowOperation).HasColumnName("FlowOperation");            
            this.Property(log => log.Comment).HasColumnName("Comment");
            this.Property(log => log.FinishTime).HasColumnName("FinishTime");
            this.Property(log => log.ArrivalTime).HasColumnName("ArrivalTime");      
        }
    }
}
