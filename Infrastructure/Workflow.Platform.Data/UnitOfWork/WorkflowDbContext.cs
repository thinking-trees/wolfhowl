/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowDbContext.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-12
 *   职    责   ：   工作流实体工作单元接口，用于访问数据
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

using Workflow.Domain.DomainObjects;
using Workflow.Platform.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Workflow.Platform.Data.UnitOfWork
{
    /// <summary>
    /// 工作流实体工作单元接口，用于访问数据
    /// </summary>
    public class WorkflowDbContext : DbContext
    {
        #region 属性
        /// <summary>
        /// 工作流数据库架构名称
        /// </summary>
        public string DatabaseSchema { get; set; }
        #endregion 属性

        #region 构造函数
        public WorkflowDbContext(string nameOrConnectionString, string dbSchema) : base(nameOrConnectionString) 
        {
            this.DatabaseSchema = dbSchema;
        }
        #endregion 构造函数

        #region 自定义对象关系映射配置
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new WorkflowInstanceInfoTypeMap("System.Activities.DurableInstancing"));
            modelBuilder.Configurations.Add(new ProcessingInstanceTypeMap(this.DatabaseSchema));
            modelBuilder.Configurations.Add(new ProcessedInstanceTypeMap(this.DatabaseSchema));
            modelBuilder.Configurations.Add(new WorkflowInstanceLogTypeMap(this.DatabaseSchema));
            modelBuilder.Configurations.Add(new WorkflowInfoTypeMap(this.DatabaseSchema));
            modelBuilder.Configurations.Add(new ActivityInfoTypeMap(this.DatabaseSchema));
            modelBuilder.Configurations.Add(new WorkflowRoleTypeMap(this.DatabaseSchema));
            modelBuilder.Configurations.Add(new HastenTaskTypeMap(this.DatabaseSchema));
            modelBuilder.Configurations.Add(new HastenSettingTypeMap(this.DatabaseSchema));
            modelBuilder.Configurations.Add(new HastenHistoryTypeMap(this.DatabaseSchema));
            modelBuilder.Configurations.Add(new SerialNumberTypeMap(this.DatabaseSchema));
        }
        #endregion

        #region 工作流实体集
        /// <summary>
        /// 工作流实例列表
        /// </summary>
        public IDbSet<WorkflowInstanceInfo> WorkflowInstanceInfos { get; set; }

        /// <summary>
        /// 待办流程任务列表
        /// </summary>
        public IDbSet<ProcessingInstance> ProcessingInstances { get; set; }

        /// <summary>
        /// 已办流程任务列表
        /// </summary>
        public IDbSet<ProcessedInstance> ProcessedInstances { get; set; }

        /// <summary>
        /// 流程实例日志列表
        /// </summary>
        public IDbSet<WorkflowInstanceLog> WorkflowInstanceLogs { get; set; }

        /// <summary>
        /// 流程类型信息列表
        /// </summary>
        public IDbSet<WorkflowInfo> WorkflowInfos { get; set; }

        /// <summary>
        /// 流程环节信息列表
        /// </summary>
        public IDbSet<ActivityInfo> ActivityInfos { get; set; }

        /// <summary>
        /// 流程角色/用户列表
        /// </summary>
        public IDbSet<WorkflowRole> WorkflowRoles { get; set; }

        /// <summary>
        /// 催单任务列表
        /// </summary>
        public IDbSet<HastenTask> HastenTasks { get; set; }

        /// <summary>
        /// 催单配置列表
        /// </summary>
        public IDbSet<HastenSetting> HastenSettings { get; set; }

        /// <summary>
        /// 催单历史列表
        /// </summary>
        public IDbSet<HastenHistory> HastenHistories { get; set; }
        #endregion

        #region 辅助管理实体集
        /// <summary>
        /// 编号管理实体列表
        /// </summary>
        public IDbSet<SerialNumber> SerialNumbers { get; set; }
        #endregion
    }
}
