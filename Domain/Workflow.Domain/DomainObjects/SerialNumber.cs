/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   SerialNumber.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-04-12
 *   职    责   ：   编号管理实体
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
using System.Collections.Generic;

namespace Workflow.Domain.DomainObjects
{
    /// <summary>
    /// 编号管理实体
    /// </summary>
    public class SerialNumber
    {
        #region 属性
        /// <summary>
        /// ID（主键）
        /// </summary>
        public string SerialNoID { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        #endregion
    }
}
