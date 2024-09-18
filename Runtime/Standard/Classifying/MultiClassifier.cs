using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    /// <summary>
    /// 多可分类，用于将一组值分类到基于键的列表中。
    /// </summary>
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public interface IMultiClassifiable<TKey, TValue> : IClassifiable<TKey, TValue, Dictionary<TKey, List<TValue>>> { }

    /// <summary>  
    /// 多分类器基类，实现了 <see cref="IMultiClassifiable{TKey, TValue}"/>，提供了分类的默认实现。  
    /// </summary>  
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>  
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public abstract class MultiClassifier<TKey, TValue> : ClassifierBase<TKey, TValue, Dictionary<TKey, List<TValue>>>, IMultiClassifiable<TKey, TValue>
    {
        protected override void OnTraverse(TValue value, Dictionary<TKey, List<TValue>> context)
        {
            TKey key = GenerateKey(value, context);

            if (!context.ContainsKey(key)) context.Add(key, new() { value });
            else context[key].Add(value);
        }
    }
}
