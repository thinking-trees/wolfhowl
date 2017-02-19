/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   ActivityInfo.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   流程环节信息实体
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-17        1.0.0.0        余树杰        初版　 
 *   2015-05-18        1.1.0.0        余树杰        增加ActivityType字段，可根据ActivityType对环节进行特定的配置 
 *   2015-05-21        1.2.0.0        余树杰        增加ActivityState字段，便于工作流运行时更准确地获取Activity信息
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;

namespace Workflow.Domain.DomainObjects
{
    /// <summary>
    /// 流程环节信息
    /// </summary>
    public class ActivityInfo
    {
        #region 属性
        /// <summary>
        /// ID（主键）
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 环节编号
        /// </summary>
        public int ActivityID { get; set; }

        /// <summary>
        /// 环节类型:Initialize,Perform,Hasten...
        /// 可根据ActivityType对环节进行特定的配置
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        /// 环节的状态值，在流程中必须唯一
        /// </summary>
        public string ActivityState { get; set; }

        /// <summary>
        /// 环节名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 处理人群组
        /// </summary>
        public string OperatorGroup { get; set; }

        /// <summary>
        /// 流程类型编号
        /// </summary>
        public int WorkflowID { get; set; }
        #endregion 属性
    }
}
