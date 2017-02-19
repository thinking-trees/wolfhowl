/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowInstanceLog.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   流程实例日志
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
using System.Collections.Generic;

namespace Workflow.Domain.DomainObjects
{
    /// <summary>
    /// 流程实例日志
    /// </summary>
    public class WorkflowInstanceLog
    {
        #region 属性
        /// <summary>
        /// 流程实例日志ID
        /// </summary>
        public string InstanceLogID { get; set; }

        /// <summary>
        /// 流程实例ID
        /// </summary>
        public string InstanceID { get; set; }

        /// <summary>
        /// 处理环节名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 环节类型:Initialize,Perform,Hasten...
        /// </summary>
        public string ActivityType { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        public string OperatorID { get; set; }

        /// <summary>
        /// 处理人姓名
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 处理人的操作指示
        /// </summary>
        public string FlowOperation { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 当前任务到达时间
        /// </summary>
        public DateTime? ArrivalTime { get; set; }

        /// <summary>
        /// 处理完成时间
        /// </summary>
        public DateTime FinishTime { get; set; }
        #endregion 属性
    }
}
