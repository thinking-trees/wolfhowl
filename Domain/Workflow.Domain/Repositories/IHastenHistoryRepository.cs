/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   IHastenHistoryRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-05-19
 *   职    责   ：   规范催单历史记录仓储接口的行为
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-05-19        1.0.0.0        余树杰        初版　 
 *    
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using Workflow.Domain.DomainObjects;

namespace Workflow.Domain.Repositories
{
    public interface IHastenHistoryRepository : IRepository<HastenHistory>
    {

    }
}
