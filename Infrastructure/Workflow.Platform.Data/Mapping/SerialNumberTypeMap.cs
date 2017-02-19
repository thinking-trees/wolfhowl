/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   SerialNumberTypeMap.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-04-12
 *   职    责   ：   配置SerialNumber类与数据库的映射关系
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-04-12        1.0.0.0        余树杰        初版　 
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
    /// 编号管理实体映射配置类
    /// </summary>
    public class SerialNumberTypeMap : EntityTypeConfiguration<SerialNumber>
    {
        public SerialNumberTypeMap(string dbSchema)
        {
            this.ToTable("SerialNumbers", dbSchema);
            this.HasKey(no => no.SerialNoID);
            this.Property(no => no.SerialNoID).HasColumnName("SerialNumberID");
            this.Property(no => no.SerialNo).HasColumnName("SerialNumber");
            this.Property(no => no.CreationTime).HasColumnName("CreationTime");
        }
    }
}
