/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   DefaultHastenService.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-19
 *   职    责   ：   系统默认的工作流催单服务
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-05-19        1.0.0.0        余树杰        初版　 
 *   2015-08-14        1.0.0.1        余树杰        增加工作流平台日志记录
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using Workflow.Extensions;
using Workflow.Domain.DomainObjects;
using Workflow.Domain.Repositories;
using Workflow.Platform.Common.Helper;

namespace Workflow.Platform.Core.Hasten
{
    /// <summary>
    /// 系统默认的工作流催单服务
    /// </summary>
    public class DefaultHastenService : IHastenService
    {
        #region 属性
        /// <summary>
        /// 工作流催单任务仓储
        /// </summary>
        public IHastenTaskRepository HastenTaskRepository { get; set; }

        /// <summary>
        /// 工作流催单配置仓储
        /// </summary>
        public IHastenSettingRepository HastenSettingRepository { get; set; }
        #endregion 属性

        #region 构造函数
        public DefaultHastenService(IHastenTaskRepository hastenTaskRepository, IHastenSettingRepository settingRepository)
        {
            this.HastenTaskRepository = hastenTaskRepository;
            this.HastenSettingRepository = settingRepository;
        }
        #endregion 构造函数

        #region 催单服务
        /// <summary>
        /// 执行催单服务
        /// </summary>
        public void Execute(HastenServiceParams svcParams)
        {
            try
            {
                bool isHastenTaskExist = this.HastenTaskRepository.CheckHastenTask(svcParams.InstanceID, svcParams.HastenActivityID);
                if (!isHastenTaskExist)
                {//新建一个催单任务
                    HastenTask hasten = new HastenTask
                    {
                        HastenTaskID = Guid.NewGuid().ToString(),
                        InstanceID = svcParams.InstanceID,
                        InstanceName = svcParams.InstanceName,
                        PerformActivityID = svcParams.PerformActivityID,
                        PerformActivityArrivalTime = svcParams.PerformActivityArrivalTime,
                        HastenActivityID = svcParams.HastenActivityID,
                        HastenAmount = svcParams.HastenAmount
                    };
                    HastenTaskCollection.HastenTasks.Add(hasten);
                    this.HastenTaskRepository.Create(hasten);
                    this.HastenTaskRepository.Commit();
                }
            }
            catch (Exception ex)
            {
                this.HastenTaskRepository.Rollback();
                System.Diagnostics.Debug.WriteLine(ex, "DefaultHastenService");
                WorkflowLogger.Error(ex);
            }
        }
        #endregion 催单服务
    }
}
