using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApi1.Domains;
using WebApi1.Engine;
using WebApi1.EnumBase;
using WebApi1.Identity;
using WebApi1.InterFace;
using WebApi1.Resource;
using WebApi1.Utility;



namespace WebApi1.SqlSugarBase
{
    public class SqlSugarRepository : IRepository
    {
        /// <summary>
        /// 连接信息
        /// </summary>
        SqlSugarClient _client;

        /// <summary>
        /// 会话信息
        /// </summary>
        protected ISession session;

        /// <summary>
        /// 单元事务管理
        /// </summary>
        protected IUnitOfWorkManager unitManager { get; }

        /// <summary>
        /// 是否单元事务
        /// </summary>
        protected bool isUnitOfWork { get { return !unitManager.IsNull() && !unitManager.Current.IsNull(); } }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="session"></param>
        /// <param name="unitMgr"></param>
        /// <param name="connection"></param>
        public SqlSugarRepository(ISession session, RepositoryConnection connection)
        {
            unitManager = EngineHelper.Resolve<IUnitOfWorkManager>();

            this.session = session;
            Connection = connection;

            _client = SqlSugarHelper.GetContext(Connection);

        }
        public SqlSugarRepository(RepositoryConnection connection)
        {
            //unitManager = EngineHelper.Resolve<IUnitOfWorkManager>();

         
            Connection = connection;

            _client = SqlSugarHelper.GetContext(Connection);

        }
        /// <summary>
        /// 连接信息
        /// </summary>
        public RepositoryConnection Connection { get; private set; }

        /// <summary>
        /// 连接信息
        /// </summary>
        /// <returns></returns>
        public SqlSugarClient GetRepository()
        {
            if (unitManager is null)
            {
                return _client;
            }
            if (unitManager.Current is SqlSugarUnitOfWork)
            {
                var unit = unitManager.Current as SqlSugarUnitOfWork;
                if (unit != null && unit.Client != null)
                {
                    return unit.Client;
                }
            }
            return _client;
        }


        #region 脚本执行

        /// <summary>
        /// 脚本执行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual int SqlExecuteCommand(string sql, params DbParameter[] parameters)
        {
            try
            {
                return Execute<int>(() =>
                {
                    return GetRepository().Ado.ExecuteCommand(sql, parameters);
                });
            }
            catch (Exception ex)
            {
                throw new CodeException(EnumCode.执行错误, ex);
            }
        }

        /// <summary>
        /// 脚本查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual dynamic SqlQueryDynamic(string sql, params DbParameter[] parameters)
        {
            try
            {
                return Execute<dynamic>(() =>
                {
                    return GetRepository().Ado.SqlQueryDynamic(sql, parameters);
                });
            }
            catch (Exception ex)
            {
                throw new CodeException(EnumCode.查询错误, ex);
            }
        }

        #endregion

        #region 事务执行

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="iso"></param>
        /// <param name="transactionName"></param>
        public virtual void BeginTran(IsolationLevel? iso = null, string transactionName = null)
        {
            if (iso.IsNull() && transactionName.IsEmpty())
            {
                GetRepository().Ado.BeginTran();
                return;
            }

            if (!iso.IsNull())
            {
                GetRepository().Ado.BeginTran(iso.Value);
                return;
            }

            if (!transactionName.IsEmpty())
            {
                GetRepository().Ado.BeginTran(transactionName);
                return;
            }
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public virtual void RollbackTran()
        {
            GetRepository().Ado.RollbackTran();
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public virtual void CommitTran()
        {
            GetRepository().Ado.CommitTran();
        }

        #endregion


        #region 获取对象

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>T 对象 or null</returns>
        public virtual T Get<T>(Expression<Func<T, bool>> where)
        {
            return Get<T>(where, null);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where">表达式</param>
        /// <param name="sorting"></param>
        /// <returns>T 对象 or null</returns>
        public virtual T Get<T>(Expression<Func<T, bool>> where, List<KeyValues> sorting)
        {
            //超过1条,使用Single会报错，First不会报错
            return GetQueryable(where: where, sorting: sorting).First();
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>T 对象 or null</returns>
        public virtual async Task<T> GetAsync<T>(Expression<Func<T, bool>> where)
        {
            return await GetAsync<T>(where, null);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="where">表达式</param>
        /// <param name="sorting"></param>
        /// <returns>T 对象 or null</returns>
        public virtual async Task<T> GetAsync<T>(Expression<Func<T, bool>> where, List<KeyValues> sorting)
        {
            //超过1条,使用Single会报错，First不会报错
            return await GetQueryable(where: where, sorting: sorting).FirstAsync();
        }

        #endregion

        #endregion

        #region 查找集合

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>IQueryable[T] 集合 new List[T]</returns>
        public virtual List<T> Find<T>(Expression<Func<T, bool>> where)
        {
            return Find(0, where: where, sorting: null);
        }

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>IQueryable[T] 集合 new List[T]</returns>
        public virtual List<T> Find<T>(Expression<Func<T, bool>> where, List<KeyValues> sorting)
        {
            return Find(0, where: where, sorting: sorting);
        }

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="top">数量</param>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[T] 集合 new List[T]</returns>
        public virtual List<T> Find<T>(int top, Expression<Func<T, bool>> where, List<KeyValues> sorting)
        {
            ISugarQueryable<T> query = GetQueryable<T>(where: where, sorting: sorting);

            if (top > 0)
            {
                query = query.Take(top);
            }

            return query.ToList();
        }

        #region 异步(可等待)操作

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="where">表达式</param>
        /// <returns>IQueryable[T] 集合 or new List[T]</returns>
        public virtual async Task<List<T>> FindAsync<T>(Expression<Func<T, bool>> where)
        {
            return await FindAsync<T>(0, where, null);
        }

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="top"></param>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[T] 集合 or new List[T]</returns>
        public virtual async Task<List<T>> FindAsync<T>(Expression<Func<T, bool>> where, List<KeyValues> sorting)
        {
            return await FindAsync<T>(0, where, sorting);
        }

        /// <summary>
        /// 集合查询
        /// </summary>
        /// <param name="top"></param>
        /// <param name="where">表达式</param>
        /// <param name="sorting">排序</param>
        /// <returns>IQueryable[T] 集合 or new List[T]</returns>
        public virtual async Task<List<T>> FindAsync<T>(int top, Expression<Func<T, bool>> where, List<KeyValues> sorting)
        {
            ISugarQueryable<T> query = GetQueryable<T>(where: where, sorting: sorting);

            if (top > 0)
            {
                query = query.Take(top);
            }

            return await query.ToListAsync();
        }

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
        public virtual List<T> Paging<T>(int page, int size, Expression<Func<T, bool>> where, List<KeyValues> sorting, ref int total)
        {
            return GetQueryable<T>(where: where, sorting: sorting).ToPageList(page, size, ref total);
        }

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
        /// 4.9代码
        //public virtual async Task<KeyValuePair<List<T>, int>> PagingAsync<T>(int page, int size, Expression<Func<T, bool>> where, List<KeyValues> sorting, int total)
        //{

        //    return await GetQueryable<T>(where: where, sorting: sorting).ToPageListAsync(page, size, total);
        //}
        ///5.0代码
        public virtual async Task<List<T>> PagingAsync<T>(int page, int size, Expression<Func<T, bool>> where, List<KeyValues> sorting, int total)
        {

            return await GetQueryable<T>(where: where, sorting: sorting).ToPageListAsync(page, size, total);
        }
        #endregion

        #endregion


        #region 存在判断

        /// <summary>
        /// 存在判断
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual bool Any<T>(Expression<Func<T, bool>> where)
        {
            return GetQueryable<T>(where: where).Any();
        }

        /// <summary>
        /// 存在判断
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> where)
        {
            return await GetQueryable<T>(where: where).AnyAsync();
        }

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
        public virtual TResult Max<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var source = GetQueryable<T>(where: where);

            if (!field.IsNull())
            {
                return source.Max<TResult>(field);
            }
            else
            {
                return source.Max<TResult>(name);
            }
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual async Task<TResult> MaxAsync<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var source = GetQueryable<T>(where: where);

            if (!field.IsNull())
            {
                return await source.MaxAsync<TResult>(field);
            }
            else
            {
                return await source.MaxAsync<TResult>(name);
            }
        }

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
        public virtual TResult Min<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var query = GetQueryable<T>(where: where);

            if (!field.IsNull())
            {
                return query.Min<TResult>(field);
            }
            else
            {
                return query.Min<TResult>(name);
            }
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual async Task<TResult> MinAsync<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var query = GetQueryable<T>(where: where);

            if (!field.IsNull())
            {
                return await query.MinAsync<TResult>(field);
            }
            else
            {
                return await query.MinAsync<TResult>(name);
            }
        }

        #endregion

        #region 总数计算

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public virtual int Count<T>(Expression<Func<T, bool>> where = null)
        {
            return GetQueryable<T>(where: where).Count();
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAsync<T>(Expression<Func<T, bool>> where = null)
        {
            return await GetQueryable<T>(where: where).CountAsync();
        }

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
        public virtual TResult Sum<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var query = GetQueryable<T>(where: where);

            if (!field.IsNull())
            {
                return query.Sum<TResult>(field);
            }
            else
            {
                return query.Sum<TResult>(name);
            }
        }

        /// <summary>
        /// 求合计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual async Task<TResult> SumAsync<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var query = GetQueryable<T>(where: where);

            if (!field.IsNull())
            {
                return await query.SumAsync<TResult>(field);
            }
            else
            {
                return await query.SumAsync<TResult>(name);
            }
        }

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
        public virtual TResult Avg<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var query = GetQueryable<T>(where: where);

            if (!field.IsNull())
            {
                return query.Avg<TResult>(field);
            }
            else
            {
                return query.Avg<TResult>(name);
            }
        }

        /// <summary>
        /// 平均计算
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="field">查询字段 与 name 选填一个</param>
        /// <param name="name">字段名称 与 field 选填一个</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public virtual async Task<TResult> AvgAsync<T, TResult>(Expression<Func<T, TResult>> field = null, string name = null, Expression<Func<T, bool>> where = null)
        {
            if (field.IsNull() && name.IsEmpty())
                throw new ArgumentException($"{nameof(field)} && {nameof(name)} cannot be null");

            var query = GetQueryable<T>(where: where);

            if (!field.IsNull())
            {
                return await query.AvgAsync<TResult>(field);
            }
            else
            {
                return await query.AvgAsync<TResult>(name);
            }
        }

        #endregion


        /// <summary>
        /// 数据源
        /// </summary>
        /// <param name="query"></param>
        /// <param name="where"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        protected virtual ISugarQueryable<T> GetQueryable<T>(ISugarQueryable<T> query = null, Expression<Func<T, bool>> where = null, List<KeyValues> sorting = null)
        {
            if (query.IsNull())
            {
                query = GetRepository().Queryable<T>();
            }

            //if (_isCaching)
            //{
            //    query = query.WithCache();
            //}

            if (where != null)
            {
                query = query.Where(where);
            }

            if (sorting != null && sorting.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                sorting.ForEach((o) =>
                {
                    builder.AppendFormat(" {0} {1},", o.Key, SqlSugarHelper.ConvertOrderType(o.Value).GetName().ToUpper());
                });
                query = query.OrderBy(builder.TrimEnd(',').ToString());
            }

            return query;
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        protected virtual T Execute<T>(Func<T> func)
        {
            if (isUnitOfWork)
            {
                return func();
            }
            else
            {
                using (GetRepository())
                {
                    var result = func();
                    GetRepository().Close();
                    GetRepository().Dispose();
                    return result;
                    //需要用using(var db = getInstance()) 或者db.Ado.Dispose()或者db.Ado.Close()进行释放
                    //db.dispose和using整个对象都不能使用 close释放掉下次还可以开始

                }
            }
        }
    }
}