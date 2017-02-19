/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   SaveRunningInstanceData.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-12
 *   职    责   ：   保存运行中的流程实例数据并进行持久化操作
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-12        1.0.0.0        余树杰        初版　 
 *    
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Activities.Persistence;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Workflow.Extensions
{
    /// <summary>
    /// 保存运行中的流程实例数据并进行持久化操作
    /// </summary>
    public class SaveRunningInstanceData : PersistenceParticipant
    {
        #region 属性
        /// <summary>
        /// 实例名称
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        public string CreateUserID { get; set; }

        /// <summary>
        /// 创建者姓名
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 实例创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 下一环节处理人ID
        /// </summary>
        public string CurrentUserID { get; set; }

        /// <summary>
        /// 下一环节处理人姓名
        /// </summary>
        public string CurrentUserName { get; set; }

        /// <summary>
        /// 当前环节ID
        /// </summary>
        public int CurrentActivityID { get; set; }

        /// <summary>
        /// 当前环节名称
        /// </summary>
        public string CurrentActivityName { get; set; }

        /// <summary>
        /// 当前任务到达时间
        /// </summary>
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// 当前处理人受理任务时间
        /// 若系统日后需要开发待阅/已阅功能或评价处理效率时恢复使用
        /// </summary>
        //public DateTime ReceptionTime { get; set; }

        /// <summary>
        /// 处理意见
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 流程实例ID
        /// </summary>
        public string InstanceID { get; set; }

        /// <summary>
        /// 流程类型编号
        /// </summary>
        public int WorkflowID { get; set; }

        /// <summary>
        /// 流程的基础业务信息
        /// </summary>
        public string BasicBusinessInfo { get; set; }

        /// <summary>
        /// 当前环节的催单次数
        /// </summary>
        public int CurrentActivityHastenAmount { get; set; }
        #endregion 属性

        #region 更新及加载持久化数据
        /// <summary>
        /// 宿主对自定义持久性参与者调用此方法，以收集要保留的读/写值和只写值
        /// </summary>
        protected override void CollectValues(out IDictionary<XName, object> readWriteValues, out IDictionary<XName, object> writeOnlyValues)
        {
            readWriteValues = new Dictionary<XName, object>
            {
                { PersistenceConfig.NS.GetName("InstanceName"), this.InstanceName },
                { PersistenceConfig.NS.GetName("CreateUserID"), this.CreateUserID },
                { PersistenceConfig.NS.GetName("CreateUserName"), this.CreateUserName },
                { PersistenceConfig.NS.GetName("CreationTime"), this.CreationTime },
                { PersistenceConfig.NS.GetName("CurrentActivityID"), this.CurrentActivityID },
                { PersistenceConfig.NS.GetName("CurrentActivityName"), this.CurrentActivityName },
                { PersistenceConfig.NS.GetName("CurrentUserID"), this.CurrentUserID },
                { PersistenceConfig.NS.GetName("CurrentUserName"), this.CurrentUserName },
                { PersistenceConfig.NS.GetName("ArrivalTime"), this.ArrivalTime },
                { PersistenceConfig.NS.GetName("Comment"), this.Comment },
                { PersistenceConfig.NS.GetName("InstanceID"), this.InstanceID },
                { PersistenceConfig.NS.GetName("WorkflowID"), this.WorkflowID },
                { PersistenceConfig.NS.GetName("BasicBusinessInfo"), this.BasicBusinessInfo },
                { PersistenceConfig.NS.GetName("CurrentActivityHastenAmount"), this.CurrentActivityHastenAmount }
            };
            writeOnlyValues = null;
        }

        /// <summary>
        /// 宿主重新加载工作流实例时初始化相关实例数据，以便在Activity中使用
        /// </summary>
        protected override void PublishValues(IDictionary<XName, object> readWriteValues)
        {
            object initialData;
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("InstanceName"), out initialData))
                this.InstanceName = initialData.ToString();
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("CreateUserID"), out initialData))
                this.CreateUserID = initialData.ToString();
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("CreateUserName"), out initialData))
                this.CreateUserName = initialData.ToString();
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("CreationTime"), out initialData))
                this.CreationTime = DateTime.Parse(initialData.ToString());
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("CurrentActivityID"), out initialData))
                this.CurrentActivityID = int.Parse(initialData.ToString());
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("CurrentActivityName"), out initialData))
                this.CurrentActivityName = initialData.ToString();
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("CurrentUserID"), out initialData))
                this.CurrentUserID = initialData.ToString();
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("CurrentUserName"), out initialData))
                this.CurrentUserName = initialData.ToString();
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("ArrivalTime"), out initialData))
                this.ArrivalTime = DateTime.Parse(initialData.ToString());
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("Comment"), out initialData))
                this.Comment = initialData.ToString();
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("InstanceID"), out initialData))
                this.InstanceID = initialData.ToString();
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("WorkflowID"), out initialData))
                this.WorkflowID = int.Parse(initialData.ToString());
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("BasicBusinessInfo"), out initialData))
                this.BasicBusinessInfo = initialData.ToString();
            if (readWriteValues.TryGetValue(PersistenceConfig.NS.GetName("CurrentActivityHastenAmount"), out initialData))
                this.CurrentActivityHastenAmount = int.Parse(initialData.ToString());
        }
        #endregion 更新及加载持久化数据
    }
}
