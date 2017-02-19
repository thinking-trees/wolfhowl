/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   HastenHistory.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-18
 *   职    责   ：   存取催单历史记录
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
    public class HastenHistory
    {
        #region 属性
        /// <summary>
        /// ID(主键)
        /// </summary>
        public string HastenHistoryID { get; set; }

        /// <summary>
        /// 流程实例ID
        /// </summary>
        public string InstanceID { get; set; }

        /// <summary>
        /// 催单环节所服务的执行环节编号
        /// </summary>
        public int PerformActivityID { get; set; }

        /// <summary>
        /// 催单环节所服务的执行环节名称
        /// </summary>
        public string PerformActivityName { get; set; }

        /// <summary>
        /// 催单环节编号
        /// </summary>
        public int HastenActivityID { get; set; }

        /// <summary>
        /// 催单环节名称
        /// </summary>
        public string HastenActivityName { get; set; }

        /// <summary>
        /// 被催单的用户ID
        /// </summary>
        public string HastenUserID { get; set; }

        /// <summary>
        /// 被催单的用户名
        /// </summary>
        public string HastenUserName { get; set; }

        /// <summary>
        /// 催单触发时间
        /// </summary>
        public DateTime? TriggerTime { get; set; }

        /// <summary>
        /// 催单执行时间
        /// </summary>
        public DateTime HastenTime { get; set; }

        /// <summary>
        /// 催单的触发值
        /// </summary>
        public string HastenTriggerValue { get; set; }
        #endregion 属性
    }
}
