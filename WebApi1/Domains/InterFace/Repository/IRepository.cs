using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using WebApi1.Domains;
using WebApi1.Resource;

namespace WebApi1.InterFace
{
    public interface IRepository : IDependency
    {
        /// <summary>
        /// 连接信息
        /// </summary>
        RepositoryConnection Connection { get; }

        #region 脚本执行

        /// <summary>
        /// 脚本执行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int SqlExecuteCommand(string sql, params DbParameter[] parameters);

        /// <summary>
        /// 脚本查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        dynamic SqlQueryDynamic(string sql, params DbParameter[] parameters);

        #endregion

        #region 事务执行

        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTran(IsolationLevel? iso = null, string transactionName = null);

        /// <summary>
        /// 事务回滚
        /// </summary>
        void RollbackTran();

        /// <summary>
        /// 事务提交
        /// </summary>
        void CommitTran();

        #endregion


        #region 获取对象

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>T 对象 or null</returns>
        T Get<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where">表达式</param>
        /// <param name="sorting"></param>
        /// <returns>T 对象 or null</returns>
        T Get<T>(Expression<Func<T, bool>> where, List<KeyValues> sorting);

        #region 异步(可等待)操作

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>T 对象 or null</returns>
        Task<T> GetAsync<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where">表达式</param>
        /// <param name="sorting"></param>
        /// <returns>T 对象 or null</returns>
        Task<T> GetAsync<T>(Expression<Func<T, bool>> where, List<KeyValues> sorting);

        #endregion

        #endregion

        #region 查找集合

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>IQueryable[T] 集合 new List[T]</returns>
        List<T> Find<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>IQueryable[T] 集合 new List[T]</returns>
        List<T> Find<T>(Expression<Func<T, bool>> where, List<KeyValues> sorting);

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="top">数量</param>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[T] 集合 new List[T]</returns>
        List<T> Find<T>(int top, Expression<Func<T, bool>> where, List<KeyValues> sorting);

        #region 异步(可等待)操作

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>IQueryable[T] 集合 or new List[T]</returns>
        Task<List<T>> FindAsync<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="top"></param>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[T] 集合 or new List[T]</returns>
        Task<List<T>> FindAsync<T>(Expression<Func<T, bool>> where, List<KeyValues> sorting);

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="top"></param>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[T] 集合 or new List[T]</returns>
        Task<List<T>> FindAsync<T>(int top, Expression<Func<T, bool>> where, List<KeyValues> sorting);

        #endregion

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="where"></param>
        /// <param name="sorting"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<T> Paging<T>(int page, int size, Expression<Func<T, bool>> where, List<KeyValues> sorting, ref int total);

        #region #region 异步(可等待)操作

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="where"></param>
        /// <param name="sorting"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        //Task<KeyValuePair<List<T>, int>> PagingAsync<T>(int page, int size, Expression<Func<T, bool>> where, List<KeyValues> sorting, int total);
        Task<List<T>> PagingAsync<T>(int page, int size, Expression<Func<T, bool>> where, List<KeyValues> sorting, int total);
        #endregion

        #endregion


        #region 存在判断

        /// <summary>
        /// 存在判断
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Any<T>(Expression<Func<T, bool>> where);

        /// <summary>
        /// 存在判断
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> where);

        #endregion

        #region 取最大值

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        TResult Max<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null);

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        Task<TResult> MaxAsync<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null);

        #endregion

        #region 取最小值

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        TResult Min<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null);

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        Task<TResult> MinAsync<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null);

        #endregion

        #region 总数计算

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        int Count<T>(Expression<Func<T, bool>> where = null);

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync<T>(Expression<Func<T, bool>> where = null);

        #endregion

        #region 求合计算

        /// <summary>
        /// 求合计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        TResult Sum<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null);

        /// <summary>
        /// 求合计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        Task<TResult> SumAsync<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null);

        #endregion

        #region 平均计算

        /// <summary>
        /// 平均计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        TResult Avg<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null);

        /// <summary>
        /// 平均计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        Task<TResult> AvgAsync<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null);

        #endregion
    }

    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IRepository<TEntity, TKey> :
        IRepository where TEntity : class, IEntity
    {
        #region 获取对象

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns>TEntity 对象 or null</returns>
        TEntity Get(TKey key);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>TEntity 对象 or null</returns>
        TEntity Get(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        TEntity Get(Expression<Func<TEntity, bool>> where, List<KeyValues> sorting);

        #region 异步(可等待)操作

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>TEntity 对象 or null</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where, List<KeyValues> sorting);

        #endregion

        #endregion

        #region 查找集合

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>IQueryable[TEntity] 集合 or new List[TEntity]</returns>
        List<TEntity> Find(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 or new List[TEntity]</returns>
        List<TEntity> Find(Expression<Func<TEntity, bool>> where, List<KeyValues> sorting);

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="top"></param>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 or new List[TEntity]</returns>
        List<TEntity> Find(int top, Expression<Func<TEntity, bool>> where, List<KeyValues> sorting);

        #region 异步(可等待)操作

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>IQueryable[TEntity] 集合 or new List[TEntity]</returns>
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 or new List[TEntity]</returns>
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> where, List<KeyValues> sorting);

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="top"></param>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[TEntity] 集合 or new List[TEntity]</returns>
        Task<List<TEntity>> FindAsync(int top, Expression<Func<TEntity, bool>> where, List<KeyValues> sorting);

        #endregion

        #endregion

        #region 分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="where"></param>
        /// <param name="sorting"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        List<TEntity> Paging(int page, int size, Expression<Func<TEntity, bool>> where, List<KeyValues> sorting, ref int total);

        #region 异步(可等待)操作

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="where"></param>
        /// <param name="sorting"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<KeyValuePair<List<TEntity>, int>> PagingAsync(int page, int size, Expression<Func<TEntity, bool>> where, List<KeyValues> sorting, int total);

        #endregion

        #endregion

        #region 插入对象

        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="entity">对象</param>
        int Insert(TEntity entity);

        /// <summary>
        /// 插入集合
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int Insert(List<TEntity> entities);

        #region 异步(可等待)操作

        /// <summary>
        /// 插入对象-(可等待)
        /// </summary>
        /// <param name="entity">对象</param>
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// 插入集合-(可等待)
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> InsertAsync(List<TEntity> entities);

        #endregion

        #endregion

        #region 更新对象

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="entity">对象</param>
        int Update(TEntity entity);

        /// <summary>
        /// 更新集合
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int Update(List<TEntity> entities);

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="columns">更新例</param>
        /// <param name="setValueExpression">设置值</param>
        /// <param name="entity">对象实体</param>
        /// <returns></returns>
        int Update(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> columns = null, Expression<Func<TEntity, bool>> setValueExpression = null, TEntity entity = null);

        #region 异步(可等待)操作

        /// <summary>
        /// 更新对象-(可等待)
        /// </summary>
        /// <param name="entity">对象</param>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// 更新集合-(可等待)
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(List<TEntity> entities);

        /// <summary>
        /// 更新操作-(可等待)
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="columns">更新例</param>
        /// <param name="setValueExpression">设置值</param>
        /// <param name="entity">对象实体</param>
        /// <returns></returns>
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> columns = null, Expression<Func<TEntity, bool>> setValueExpression = null, TEntity entity = null);

        #endregion

        #endregion

        #region 删除对象

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id">Id</param>
        int Delete(TKey id);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">Ids</param>
        int Delete(List<TKey> ids);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">对象</param>
        int Delete(TEntity entity);

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="entities">表达式</param>
        int Delete(List<TEntity> entities);

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="where">表达式</param>
        int Delete(Expression<Func<TEntity, bool>> where);

        #region 异步(可等待)操作

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id">Id</param>
        Task<int> DeleteAsync(TKey id);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="ids">Ids</param>
        Task<int> DeleteAsync(List<TKey> ids);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="entity">对象</param>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="entities">表达式</param>
        Task<int> DeleteAsync(List<TEntity> entities);

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <param name="where">表达式</param>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> where);

        #endregion

        #endregion
    }

    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IRepository<TEntity> :
        IRepository<TEntity, Guid> where TEntity : class, IEntity
    {
    }
}