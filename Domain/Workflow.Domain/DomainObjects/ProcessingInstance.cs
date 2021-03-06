﻿/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   ProcessingInstance.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-16
 *   职    责   ：   待办流程任务实体
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-16        1.0.0.0        余树杰        初版　 
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
    /// 待办流程任务实体
    /// </summary>
    public class ProcessingInstance
    {
        #region 属性
        /// <summary>
        /// 流程实例ID
        /// </summary>
        public Guid InstanceID { get; set; }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// 流程实例类型
        /// </summary>
        public string InstanceType { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        public string CreateUserID { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 当前环节处理人ID
        /// </summary>
        public string CurrentUserID { get; set; }

        /// <summary>
        /// 当前环节处理人姓名
        /// </summary>
        public string CurrentUserName { get; set; }

        /// <summary>
        /// 当前环节编号
        /// </summary>
        public int CurrentActivityID { get; set; }

        /// <summary>
        /// 当前环节名称
        /// </summary>
        public string CurrentActivityName { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool? IsCompleted { get; set; }
        #endregion 属性
    }
}
