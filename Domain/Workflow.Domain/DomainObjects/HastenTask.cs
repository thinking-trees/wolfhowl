﻿/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   HastenTask.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-18
 *   职    责   ：   催单任务实体
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
    public class HastenTask
    {
        /// <summary>
        /// ID（主键）
        /// </summary>
        public string HastenTaskID { get; set; }

        /// <summary>
        /// 流程实例ID
        /// </summary>
        public string InstanceID { get; set; }

        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// 催单所服务的执行环节编号
        /// </summary>
        public int PerformActivityID { get; set; }

        /// <summary>
        /// 执行环节的任务到达时间
        /// </summary>
        public DateTime PerformActivityArrivalTime { get; set; }

        /// <summary>
        /// 催单环节编号
        /// </summary>
        public int HastenActivityID { get; set; }

        /// <summary>
        /// 当前执行环节的催单次数
        /// </summary>
        public int HastenAmount { get; set; }
    }
}
