/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   HastenSetting.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-18
 *   职    责   ：   催单配置实体
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
using System.Collections.Generic;

namespace Workflow.Domain.DomainObjects
{
    public class HastenSetting
    {
        #region 属性
        /// <summary>
        /// ID（主键）
        /// </summary>
        public string HastenSettingID { get; set; }

        /// <summary>
        /// 催单环节编号
        /// </summary>
        public int HastenActivityID { get; set; }

        /// <summary>
        /// 催单级别：1级(一般),2级(重要),3级(紧急)
        /// 选择高级别催单时，低级别催单也需要进行设置
        /// </summary>
        public Int16 HastenLevel { get; set; }

        /// <summary>
        /// 连续催办的时间间隔，单位:minute
        /// 最高级别的催单动作执行完毕后，HastenInterval才计算进触发时间
        /// 后续每次催单均执行最高级别的催单动作
        /// </summary>
        public int HastenInterval { get; set; }

        /// <summary>
        /// 1级催单的触发值，流程根据此值流转
        /// 该值由用户在催单环节中设置
        /// </summary>
        public string LevelOneTriggerValue { get; set; }

        /// <summary>
        /// 1级催单的服务时限,单位:minute
        /// 1级催单对象默认为当前环节处理人
        /// </summary>
        public int LevelOneTimeLimit { get; set; }

        /// <summary>
        /// 2级催单的触发值，流程根据此值流转
        /// 该值由用户在催单环节中设置
        /// </summary>
        public string LevelTwoTriggerValue { get; set; }

        /// <summary>
        /// 2级催单的服务时限,单位:minute
        /// 2级催单的服务时限为LevelOneTimeLimit+LevelTwoTimeLimit
        /// </summary>
        public int? LevelTwoTimeLimit { get; set; }

        /// <summary>
        /// 3级催单的触发值，流程根据此值流转
        /// 该值由用户在催单环节中设置
        /// </summary>
        public string LevelThreeTriggerValue { get; set; }

        /// <summary>
        /// 3级催单的服务时限,单位:minute
        /// 3级催单的服务时限为LevelOneTimeLimit+LevelTwoTimeLimit+LevelThreeTimeLimit
        /// </summary>
        public int? LevelThreeTimeLimit { get; set; }

        /// <summary>
        /// 配置更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        #endregion 属性
    }
}
