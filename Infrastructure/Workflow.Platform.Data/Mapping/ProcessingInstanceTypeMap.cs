/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   ProcessingInstanceTypeMap.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-16
 *   职    责   ：   配置ProcessingInstance类与数据库的映射关系
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-16        1.0.0.0        余树杰        初版　 
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
    /// 待办流程任务实体映射配置类
    /// </summary>
    public class ProcessingInstanceTypeMap : EntityTypeConfiguration<ProcessingInstance>
    {
        public ProcessingInstanceTypeMap(string dbSchema)
        {
            this.ToTable("ProcessingInstances", dbSchema);
            this.HasKey(instance => instance.InstanceID);
            this.Property(instance => instance.InstanceID).HasColumnName("InstanceID");
            this.Property(instance => instance.InstanceName).HasColumnName("InstanceName");
            this.Property(instance => instance.InstanceType).HasColumnName("InstanceType");
            this.Property(instance => instance.CreateUserID).HasColumnName("CreateUserID");
            this.Property(instance => instance.CreateUserName).HasColumnName("CreateUserName");
            this.Property(instance => instance.CreationTime).HasColumnName("CreationTime");
            this.Property(instance => instance.CurrentUserID).HasColumnName("CurrentUserID");
            this.Property(instance => instance.CurrentUserName).HasColumnName("CurrentUserName");
            this.Property(instance => instance.CurrentActivityID).HasColumnName("CurrentActivityID");
            this.Property(instance => instance.CurrentActivityName).HasColumnName("CurrentActivityName");
            this.Property(instance => instance.Comment).HasColumnName("Comment");
            this.Property(instance => instance.IsCompleted).HasColumnName("IsCompleted");
        }
    }
}
