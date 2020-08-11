using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApi1.EnumBase;
using WebApi1.InterFace;
using WebApi1.Resource;
using WebApi1.Utility;

namespace WebApi1.Domains.Uow
{
    public abstract class UnitOfWorkDefault : IUnitOfWork
    {
        bool _succeed;
        bool _isBeginCalledBefore;
        bool _isCompleteCalledBefore;
        Exception _exception;
        IUnitOfWork _outer;
        UnitOfWorkOptions _options = new UnitOfWorkOptions();

        /// <summary>
        /// 单元标识
        /// </summary>
        public string Id { get; private set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 是否释放
        /// </summary>
        public bool IsDisposed { get; private set; }


        /// <summary>
        /// 完成事件
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// 失败事件
        /// </summary>
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// 释放事件
        /// </summary>
        public event EventHandler Disposed;


        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="options"></param>
        public virtual void Begin(UnitOfWorkOptions options)
        {
            options.IsNull();
            _options = options;
            PreventMultipleBegin();
            BeginUow();
        }

        /// <summary>
        /// 完成事务
        /// </summary>
        public void Complete()
        {
            try
            {
                PreventMultipleComplete();
                CompleteUow();
                OnCompleted();
                _succeed = true;
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <summary>
        /// 完成事务(异步)
        /// </summary>
        /// <returns></returns>
        public async Task CompleteAsync()
        {
            try
            {
                PreventMultipleComplete();
                await CompleteUowAsync();
                OnCompleted();
                _succeed = true;
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            if (!_isBeginCalledBefore || IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (!_succeed)
            {
                OnFailed(_exception);
            }

            DisposeUow();
            OnDisposed();
        }



        /// <summary>
        /// 保存更改
        /// </summary>
        public abstract void SaveChanges();

        /// <summary>
        /// 保存更改(异常)
        /// </summary>
        public abstract Task SaveChangesAsync();



        /// <summary>
        /// 开始事务
        /// </summary>
        protected abstract void BeginUow();

        /// <summary>
        /// 完成事务
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// 完成事务(异步)
        /// </summary>
        /// <returns></returns>
        protected abstract Task CompleteUowAsync();

        /// <summary>
        /// 释放事务
        /// </summary>
        protected abstract void DisposeUow();



        /// <summary>
        /// 完成事件
        /// </summary>
        protected virtual void OnCompleted()
        {
            Completed.InvokeSafely(this);
        }

        /// <summary>
        /// 失败事件
        /// </summary>
        /// <param name="exception"></param>
        protected virtual void OnFailed(Exception exception)
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }

        /// <summary>
        /// 释放事件
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this);
        }



        /// <summary>
        /// 获取外部引用
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork GetOuter()
        {
            return _outer;
        }

        /// <summary>
        /// 设置外部引用
        /// </summary>
        /// <param name="outer"></param>
        public void SetOuter(IUnitOfWork outer)
        {
            _outer = outer;
        }



        /// <summary>
        /// 防止多次开始
        /// </summary>
        void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                throw new CodeException(EnumCode.错误, "当前工作单元已启动，不能多次调用Begin 方法");
            }

            _isBeginCalledBefore = true;
        }

        /// <summary>
        /// 防止多次完成
        /// </summary>
        void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new CodeException(EnumCode.错误, "Complete is called before!");
            }

            _isCompleteCalledBefore = true;
        }



    }
}