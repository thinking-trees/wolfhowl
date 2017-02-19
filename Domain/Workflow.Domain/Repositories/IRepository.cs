/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   IRepository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-15
 *   职    责   ：   定义仓储接口的基本行为
 *    
 *
 *　----------------------------------变更历史----------------------------------　 
 *   修改日期            版本         修改者        修改内容
 *   2015-03-15        1.0.0.0        余树杰        初版　 
 *    
 *
 *   
 *   
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workflow.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="id">实体id</param>
        TEntity Get(string id);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// 新建实体
        /// </summary>
        /// <param name="entity">实体数据</param>
        void Create(TEntity entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        void Delete(TEntity entity);

        /// <summary>
        /// 根据id删除实体
        /// </summary>
        /// <param name="id">实体id</param>
        void Delete(string id);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体数据</param>
        void Update(TEntity entity);

        /// <summary>
        /// 保存更改到数据库
        /// </summary>
        void Commit();

        /// <summary>
        /// 撤销对数据的更改
        /// </summary>
        void Rollback();
    }
}
