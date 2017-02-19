/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   SerialNumberRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-04-12
 *   职    责   ：   实现编号管理仓储接口的行为
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
using System.Linq;
using System.Text;
using Workflow.Domain.DomainObjects;
using Workflow.Domain.Repositories;
using Workflow.Platform.Data.UnitOfWork;

namespace Workflow.Platform.Data.Repositories
{
    public class SerialNumberRepository : Repository<SerialNumber>, ISerialNumberRepository
    {
        public SerialNumberRepository(WorkflowDbContext unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// 生成自动增长编号
        /// </summary>
        /// <param name="prefix">编号前辍</param>
        /// <param name="seed">标识种子</param>
        /// <param name="increment">标识增量</param>
        public string CreateNumber(string prefix, int seed = 1, int increment = 1)
        {
            SerialNumber serialNumber = new SerialNumber();
            SerialNumber query = this.CurrentUnitOfWork.SerialNumbers.FirstOrDefault(n => n.SerialNo.StartsWith(prefix));
            StringBuilder serialNoBuilder = new StringBuilder();
            try
            {
                if (null != query)
                {
                    serialNumber = query;
                    int oldNum = 0;
                    int.TryParse(query.SerialNo.Substring(prefix.Length), out oldNum);
                    serialNoBuilder.Append(string.Format("{0}{1}", prefix, oldNum + increment));
                    serialNumber.SerialNo = serialNoBuilder.ToString();
                    serialNumber.CreationTime = DateTime.Now;
                    Update(serialNumber);
                }
                else
                {
                    serialNumber.SerialNoID = Guid.NewGuid().ToString();
                    serialNoBuilder.Append(string.Format("{0}{1}", prefix, seed));
                    serialNumber.SerialNo = serialNoBuilder.ToString();
                    serialNumber.CreationTime = DateTime.Now;
                    Create(serialNumber);
                }
                Commit();
                return serialNumber.SerialNo;
            }
            catch
            {
                Rollback();
                return "";
            }
        }

        /// <summary>
        /// 生成业务流水编号
        /// </summary>
        /// <param name="prefix">编号前缀</param>
        /// <param name="dateFormat">日期格式,如：yyyyMMdd或yyMMdd</param>
        /// <param name="serialLength">流水号长度</param>
        public string CreateNumber(string prefix, string dateFormat = "yyyyMMdd", int serialLength = 4)
        {
            return "";
        }
    }
}
