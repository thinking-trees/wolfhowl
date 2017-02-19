/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowHastenInfo.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-22
 *   职    责   ：   流程执行催单服务所需参数
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-05-22        1.0.0.0        余树杰        初版　
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
    /// 流程执行催单服务所需参数
    /// </summary>
    [DataContract]
    public class WorkflowHastenInfo
    {
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
        /// 催单级别触发值，流程根据此值流转
        /// </summary>
        [DataMember]
        public object HastenLevelTriggerValue { get; set; }
    }
}
