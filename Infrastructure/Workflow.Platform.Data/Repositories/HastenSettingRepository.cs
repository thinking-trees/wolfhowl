/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   HastenSettingRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-18
 *   职    责   ：   实现催单配置仓储接口的行为
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
using System.Linq;
using Workflow.Domain.DomainObjects;
using Workflow.Domain.Repositories;
using Workflow.Platform.Data.UnitOfWork;

namespace Workflow.Platform.Data.Repositories
{
    public class HastenSettingRepository : Repository<HastenSetting>, IHastenSettingRepository
    {
        public HastenSettingRepository(WorkflowDbContext unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// 获取催单设置信息
        /// </summary>
        /// <param name="hastenActID">催单环节编号</param>
        public HastenSetting GetHastenSetting(int hastenActID)
        {
            try
            {
                return this.CurrentUnitOfWork.HastenSettings.SingleOrDefault(setting => setting.HastenActivityID.Equals(hastenActID));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex, "HastenSettingRepository");
                throw;
            }
        }
    }
}
