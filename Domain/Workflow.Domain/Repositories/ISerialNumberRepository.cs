/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   ISerialNumberRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-04-12
 *   职    责   ：   规范编号管理仓储接口的行为
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
using Workflow.Domain.DomainObjects;

namespace Workflow.Domain.Repositories
{
    public interface ISerialNumberRepository : IRepository<SerialNumber>
    {
        /// <summary>
        /// 生成自动增长编号
        /// </summary>
        /// <param name="prefix">编号前辍</param>
        /// <param name="seed">标识种子</param>
        /// <param name="increment">标识增量</param>
        string CreateNumber(string prefix, int seed = 1, int increment = 1);

        /// <summary>
        /// 生成业务流水编号
        /// </summary>
        /// <param name="prefix">编号前缀</param>
        /// <param name="dateFormat">日期格式,如：yyyyMMdd或yyMMdd</param>
        /// <param name="serialLength">流水号长度</param>
        string CreateNumber(string prefix, string dateFormat = "yyyyMMdd", int serialLength = 4);
    }
}
