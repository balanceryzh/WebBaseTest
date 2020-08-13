using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Framework
{
    public class DomainProfileManager : List<IDomainProfile> //IEnumerable<KeyValueModel<Type, IDomainProfile>>
    {
        #region IEnumerable

        ///// <summary>
        ///// ctor
        ///// </summary>
        //public DomainProfileManager()
        //{
        //}

        ///// <summary>
        ///// 配置集合
        ///// </summary>
        //readonly List<KeyValueModel<Type, IDomainProfile>> items = new List<KeyValueModel<Type, IDomainProfile>>();

        ///// <summary>
        ///// 配置集合
        ///// </summary>
        //public List<KeyValueModel<Type, IDomainProfile>> List { get { return items; } }

        ///// <summary>
        ///// 添加配置
        ///// </summary>
        //public void Add(Type key, IDomainProfile value)
        //{
        //    items.Add(new KeyValueModel<Type, IDomainProfile>(key, value));
        //}

        ///// <summary>
        ///// 添加配置列表
        ///// </summary>
        //public void AddRange(IEnumerable<KeyValueModel<Type, IDomainProfile>> collection)
        //{
        //    items.AddRange(collection);
        //}

        ///// <summary>
        ///// 插入配置
        ///// </summary>
        //public void Insert(int index, Type key, IDomainProfile value)
        //{
        //    items.Insert(index, new KeyValueModel<Type, IDomainProfile>(key, value));
        //}

        ///// <summary>
        ///// 移除配置
        ///// </summary>
        //public void Remove(Type key)
        //{
        //    var item = items.Where(x => x.Key == key).FirstOrDefault();
        //    if (!item.IsNull())
        //    {
        //        items.Remove(item);
        //    }
        //}

        ///// <summary>
        ///// 实现接口中的方法  
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerator<KeyValueModel<Type, IDomainProfile>> GetEnumerator()
        //{
        //    foreach (KeyValueModel<Type, IDomainProfile> item in items)
        //        yield return item;   //这里使用yield可以非常简化我们工作量不用我们自己去写一个继承IEnumerator 的类，这样编译器帮我做了很多事情  
        //}

        ////接口中的方法  
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

        /// <summary>
        /// 添加配置
        /// </summary>
        public void Add(params IDomainProfile[] items)
        {
            base.AddRange(items);
        }

        /// <summary>
        /// 配置安装
        /// </summary>
        public void Install()
        {
            this.ForEach(i => i.Install());
        }
    }
}