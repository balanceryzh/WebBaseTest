using SqlSugar;

using System.Threading.Tasks;
using WebApi1.Domains.Uow;
using WebApi1.Engine;
using WebApi1.InterFace;
using WebApi1.Utility;

namespace WebApi1.SqlSugarBase
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class SqlSugarUnitOfWork : UnitOfWorkDefault, IUnitOfWork
    {
        SqlSugarRepository _repository;

        /// <summary>
        /// 仓储连接对象(注意循环引用获取问题)
        /// </summary>
        public SqlSugarClient Client
        {
            get
            {
                var _outer = GetOuter();
                if (_outer != null)
                {
                    var sqlSugarUnit = _outer as SqlSugarUnitOfWork;
                    if (!sqlSugarUnit.IsNull() && !sqlSugarUnit.Client.IsNull())
                    {
                        return sqlSugarUnit.Client;
                    }
                }
                return _client;
            }
            set
            {
                _client = value;
            }
        }
        SqlSugarClient _client;


        /// <summary>
        /// 开始事务
        /// </summary>
        protected override void BeginUow()
        {
            if (GetOuter() == null)
            {
                _repository = EngineHelper.Resolve<IRepository>() as SqlSugarRepository;
                _repository?.BeginTran();
                Client = _repository?.GetRepository();
            }
        }

        /// <summary>
        /// 保存变更
        /// </summary>
        public override void SaveChanges()
        {
            //.....
        }

        /// <summary>
        /// 保存操作异步
        /// </summary>
        /// <returns></returns>
        public override Task SaveChangesAsync()
        {
            //.....
            return Task.FromResult(0);
        }



        /// <summary>
        /// 完成事务
        /// </summary>
        protected override void CompleteUow()
        {
            //由最顶层提交
            if (GetOuter() == null)
            {
                _repository?.CommitTran();
            }
        }

        /// <summary>
        /// 完成事务异步
        /// </summary>
        protected override Task CompleteUowAsync()
        {
            //由最顶层提交
            if (GetOuter() == null)
            {
                _repository?.CommitTran();
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// 释放事务
        /// </summary>
        protected override void DisposeUow()
        {
            _repository?.RollbackTran();
        }
    }
}