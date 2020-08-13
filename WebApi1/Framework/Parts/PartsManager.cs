using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Framework
{
    /// <summary>
    /// 部件管理器
    /// </summary>
    public class PartsManager : List<IPartsProfile>
    {
        #region IEnumerable

        ///// <summary>
        ///// ctor
        ///// </summary>
        //public ModuleManager()
        //{
        //}

        //private List<IModuleConfiguration> items = new List<IModuleConfiguration>();

        ///// <summary>
        ///// 集合
        ///// </summary>
        //public List<IModuleConfiguration> List { get { return items; } }

        ///// <summary>
        ///// 添加列表
        ///// </summary>
        //public void AddRange(IEnumerable<IModuleConfiguration> collection)
        //{
        //    items.AddRange(collection);
        //}

        ///// <summary>
        ///// 插入模块
        ///// </summary>
        //public void Insert(int index, IModuleConfiguration item)
        //{
        //    items.Insert(index, item);
        //}

        ///// <summary>
        ///// 移除
        ///// </summary>
        //public void Remove(IModuleConfiguration item)
        //{
        //    items.Remove(item);
        //}

        ///// <summary>
        ///// 实现接口中的方法  
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerator<IModuleConfiguration> GetEnumerator()
        //{
        //    foreach (IModuleConfiguration item in items)
        //        yield return item;   //这里使用yield可以非常简化我们工作量不用我们自己去写一个继承IEnumerator 的类，这样编译器帮我做了很多事情  
        //}

        ////接口中的方法  
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        /// <summary>
        /// 添加
        /// </summary>
        public void Add(params IPartsProfile[] items)
        {
            base.AddRange(items);
        }

        /// <summary>
        /// 安装
        /// </summary>
        public void Install()
        {
            this.ForEach(i => i.Install());
        }
    }
}