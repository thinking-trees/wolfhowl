/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   WorkflowTaskManager.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-18
 *   职    责   ：   工作流任务管理器
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-18        1.0.0.0        余树杰        初版　 
 *   2015-08-14        1.0.0.1        余树杰        增加异常处理及工作流平台日志记录 
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.Domain.DomainObjects;
using Workflow.Domain.Repositories;
using Workflow.Platform.Common.Helper;

namespace Workflow.Platform.Core
{
    /// <summary>
    /// 工作流任务管理器
    /// </summary>
    public class WorkflowTaskManager
    {
        #region 属性
        /// <summary>
        /// 待办任务仓储
        /// </summary>
        public IProcessingInstanceRepository ProcessingInstanceRepository { get; set; }

        /// <summary>
        /// 已办任务仓储
        /// </summary>
        public IProcessedInstanceRepository ProcessedInstanceRepository { get; set; }
        #endregion

        #region 构造函数
        public WorkflowTaskManager(IProcessingInstanceRepository procInstanceRepository, IProcessedInstanceRepository procedInstanceRepository)
        {
            this.ProcessingInstanceRepository = procInstanceRepository;
            this.ProcessedInstanceRepository = procedInstanceRepository;
        }
        #endregion 构造函数

        #region 任务管理
        /// <summary>
        /// 获取待办流程任务列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        public List<ProcessingInstance> GetProcessingInstances(string userID)
        {
            try
            {
                var instances = this.ProcessingInstanceRepository.GetProcessingInstances(userID);
                return instances.ToList();
            }
            catch (Exception ex)
            {
                WorkflowLogger.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// 获取已办流程任务列表
        /// </summary>
        /// <param name="userID">处理人ID</param>
        public List<ProcessedInstance> GetProcessedInstances(string userID)
        {
            try
            {
                var instances = this.ProcessedInstanceRepository.GetProcessedInstances(userID);
                return instances.ToList();
            }
            catch (Exception ex)
            {
                WorkflowLogger.Error(ex);
                return null;
            }
        }
        #endregion 任务管理
    }
}
