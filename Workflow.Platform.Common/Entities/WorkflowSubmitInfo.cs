/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowSubmitInfo.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-12
 *   职    责   ：   定义提交流程所需参数
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-12        1.0.0.0        余树杰        初版　 
 *   2015-09-10        1.1.0.0        余树杰        移除InstanceName属性，该属性在InitializeActivity创建后不可再更改
 *    
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Workflow.Platform.Common.Entities
{
    /// <summary>
    /// 提交流程所需参数实体
    /// </summary>
    [DataContract]
    public class WorkflowSubmitInfo
    {
        #region 属性
        /// <summary>
        /// 工作流类型的完全限定名称
        /// </summary>
        [DataMember]
        public string WorkflowFullName { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        [DataMember]
        public string InstanceID { get; set; }

        /// <summary>
        /// 下一环节ID
        /// </summary>
        [DataMember]
        public int NextActivityID { get; set; }

        /// <summary>
        /// 下一环节处理人ID
        /// </summary>
        [DataMember]
        public string NextUserID { get; set; }

        /// <summary>
        /// 下一环节处理人姓名
        /// </summary>
        [DataMember]
        public string NextUserName { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        [DataMember]
        public string Comment { get; set; }

        /// <summary>
        /// 流程的基础业务信息
        /// 格式：key#:#value#,#key#:#value#,#key#:#value
        /// </summary>
        [DataMember]
        public string BasicBusinessInfo { get; set; }

        /// <summary>
        /// 流程处理操作（值随具体流程而定）
        /// </summary>
        [DataMember]
        public object FlowOperation { get; set; }
        #endregion 属性
    }
}
