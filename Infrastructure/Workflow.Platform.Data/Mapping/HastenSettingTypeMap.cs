/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   HastenSettingTypeMap.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-18
 *   职    责   ：   配置HastenSetting类与数据库的映射关系
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
    public class HastenSettingTypeMap : EntityTypeConfiguration<HastenSetting>
    {
        public HastenSettingTypeMap(string schema)
        {
            this.ToTable("HastenSettings", schema);
            this.HasKey(setting => setting.HastenSettingID);
            this.Property(setting => setting.HastenSettingID).HasColumnName("HastenSettingID");
            this.Property(setting => setting.HastenActivityID).HasColumnName("HastenActivityID");
            this.Property(setting => setting.HastenLevel).HasColumnName("HastenLevel");
            this.Property(setting => setting.HastenInterval).HasColumnName("HastenInterval");
            this.Property(setting => setting.LevelOneTriggerValue).HasColumnName("LevelOneTriggerValue");
            this.Property(setting => setting.LevelOneTimeLimit).HasColumnName("LevelOneTimeLimit");
            this.Property(setting => setting.LevelTwoTriggerValue).HasColumnName("LevelTwoTriggerValue");
            this.Property(setting => setting.LevelTwoTimeLimit).HasColumnName("LevelTwoTimeLimit");
            this.Property(setting => setting.LevelThreeTriggerValue).HasColumnName("LevelThreeTriggerValue");
            this.Property(setting => setting.LevelThreeTimeLimit).HasColumnName("LevelThreeTimeLimit");
            this.Property(setting => setting.UpdateTime).HasColumnName("UpdateTime");
        }
    }
}
