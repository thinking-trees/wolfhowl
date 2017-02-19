/********************************************************************************
 *
 *   项目名称   ：   WolfHowl - 工作流平台
 *   文 件 名   ：   Repository.cs
 *   创 建 者   ：   余树杰
 *   创建日期   ：   2015-03-15
 *   职    责   ：   实现仓储接口的基本行为
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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Workflow.Domain.Repositories;
using Workflow.Platform.Data.UnitOfWork;

namespace Workflow.Platform.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region 构造函数
        public Repository(WorkflowDbContext unitOfWork)
        {
            if (null == unitOfWork)
            {
                throw new ArgumentNullException("WorkflowDbContext");
            }
            this.CurrentUnitOfWork = unitOfWork;
        }
        #endregion 构造函数

        #region 属性
        public WorkflowDbContext CurrentUnitOfWork { get; private set; }
        #endregion 属性

        #region 实体查询方法
        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <param name="id">实体id</param>
        public TEntity Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return this.CurrentUnitOfWork.Set<TEntity>().Find(new object[] { id });
            }
            return default(TEntity);
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        public IEnumerable<TEntity> GetAll()
        {
            return this.CurrentUnitOfWork.Set<TEntity>().AsEnumerable<TEntity>();
        }
        #endregion 实体查询方法

        #region 实体增删改
        /// <summary>
        /// 新建实体
        /// </summary>
        /// <param name="entity">实体数据</param>
        public void Create(TEntity entity)
        {
            if (default(TEntity) != entity)
            {
                this.CurrentUnitOfWork.Set<TEntity>().Add(entity);
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        public void Delete(TEntity entity)
        {
            if (default(TEntity) != entity)
            {
                this.CurrentUnitOfWork.Entry<TEntity>(entity).State = EntityState.Unchanged;
                this.CurrentUnitOfWork.Set<TEntity>().Remove(entity);
            }
        }

        /// <summary>
        /// 根据id删除实体
        /// </summary>
        /// <param name="id">实体id</param>
        public void Delete(string id)
        {
            IDbSet<TEntity> dbSet = this.CurrentUnitOfWork.Set<TEntity>();
            TEntity entity = dbSet.Find(new object[] { id });
            if (null != entity)
            {
                dbSet.Remove(entity);
            }
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体数据</param>
        public void Update(TEntity entity)
        {
            if (default(TEntity) != entity)
            {
                this.CurrentUnitOfWork.Entry<TEntity>(entity).State = EntityState.Modified;
            }
        }
        #endregion 实体增删改

        #region 事务
        /// <summary>
        /// 保存更改到数据库
        /// </summary>
        public void Commit()
        {
            try
            {
                this.CurrentUnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 撤销对数据的更改
        /// </summary>
        public void Rollback()
        {
            this.CurrentUnitOfWork.ChangeTracker.Entries().ToList<DbEntityEntry>().ForEach(delegate(DbEntityEntry entry)
            {
                entry.State = EntityState.Unchanged;
            });
        }
        #endregion
    }
}
