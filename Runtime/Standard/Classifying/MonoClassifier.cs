using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    /// <summary>
    /// 单可分类，用于将一组值分类到基于键的列表中。
    /// </summary>
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public interface IMonoClassifiable<TKey, TValue> : IClassifiable<TKey, TValue, Dictionary<TKey, TValue>> { }

    /// <summary>  
    /// 单分类器基类，实现了 <see cref="IMonoClassifiable{TKey, TValue}"/>，提供了分类的默认实现。  
    /// </summary>  
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>  
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public abstract class MonoClassifier<TKey, TValue> : ClassifierBase<TKey, TValue, Dictionary<TKey, TValue>>, IMonoClassifiable<TKey, TValue>
    {
        protected override void OnTraverse(TValue value, Dictionary<TKey, TValue> context) => context.Add(GenerateKey(value, context), value);
    }
}
