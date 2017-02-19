/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowStartInfo.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-12
 *   职    责   ：   定义发起流程所需参数
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-12        1.0.0.0        余树杰        初版　 
 *   2015-05-22        1.1.0.0        余树杰        增加基础业务信息接口，方便工作流运行时获取基础业务数据
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
    /// 发起流程所需参数实体
    /// </summary>
    [DataContract]
    public class WorkflowStartInfo
    {
        #region 属性
        /// <summary>
        /// 工作流类型的完全限定名称
        /// </summary>
        [DataMember]
        public string WorkflowFullName { get; set; }

        /// <summary>
        /// 实例名称
        /// </summary>
        [DataMember]
        public string InstanceName { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [DataMember]
        public string CreateUserID { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        [DataMember]
        public string CreateUserName { get; set; }

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
        /// 工作流版本号
        /// </summary>
        //[DataMember]
        //public int Version { get; set; }
        #endregion 属性
    }
}
