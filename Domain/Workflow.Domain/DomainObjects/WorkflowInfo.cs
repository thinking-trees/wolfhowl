/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowInfo.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-17
 *   职    责   ：   流程类型信息实体
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
    /// 流程类型信息
    /// </summary>
    public class WorkflowInfo
    {
        #region 属性
        /// <summary>
        /// ID（主键）
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 流程类型编号
        /// </summary>
        public int WorkflowID { get; set; }

        /// <summary>
        /// 流程类型的完全限定名称(包括名称空间)
        /// </summary>
        public string WorkflowFullName { get; set; }

        /// <summary>
        /// 流程的业务类型名称
        /// </summary>
        public string WorkflowBusinessName { get; set; }

        /// <summary>
        /// 流程版本编号
        /// </summary>
        public short? Version { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? PublishTime { get; set; }
        #endregion 属性
    }
}
