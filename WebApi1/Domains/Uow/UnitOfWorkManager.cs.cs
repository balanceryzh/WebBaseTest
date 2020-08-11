using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi1.Engine;
using WebApi1.InterFace;

namespace WebApi1.Domains.Uow
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        readonly IUnitOfWorkProvider _provider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="provider"></param>
        public UnitOfWorkManager(IUnitOfWorkProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 获取当前活动的工作单元(如果不存在，则为空)。
        /// </summary>
        public IUnitOfWorkActive Current
        {
            get { return _provider.Current; }
        }


        /// <summary>
        /// 开始一个工作单元
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options = null)
        {
            options = options ?? new UnitOfWorkOptions();

            var outerUow = _provider.Current;
            if (outerUow != null)
            {
                //options.FillOuterUowFiltersForNonProvidedOptions(outerUow.Filters.ToList());
            }

            var unitOfWork = Create();
            _provider.Current = unitOfWork;

            unitOfWork.Begin(options);

            return unitOfWork;
        }

        /// <summary>
        /// 创建工作单元
        /// </summary>
        /// <returns></returns>
        IUnitOfWork Create()
        {
            var uow = EngineHelper.Resolve<IUnitOfWork>();
            uow.Completed += (sender, args) =>
            {
                _provider.Current = null;
            };
            uow.Failed += (sender, args) =>
            {
                _provider.Current = null;
            };
            uow.Disposed += (sender, args) =>
            {
                _provider.Current = null;
            };

            return uow;
        }
    }
}