using InitialFramework.Traverse;
using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    /// <summary>
    /// 可分类，用于将一组值分类到基于键的列表中。
    /// </summary>
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public interface IClassifiable<TKey, TValue> : IFlowTraversable<TValue, Dictionary<TKey, List<TValue>>>
    {
        /// <summary>
        /// 将一组值分类到基于键的列表中。
        /// </summary>
        Dictionary<TKey, List<TValue>> Classify(IEnumerable<TValue> values);

        /// <summary>  
        /// 为指定值生成一个键。  
        /// </summary>  
        TKey GenerateKey(TValue value, Dictionary<TKey, List<TValue>> context);
    }

    /// <summary>  
    /// 分类器基类，实现了 <see cref="IClassifiable{TKey, TValue}"/>，提供了分类的默认实现。  
    /// </summary>  
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>  
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public abstract class Classifier<TKey, TValue> : Traverser<TValue, Dictionary<TKey, List<TValue>>>, IClassifiable<TKey, TValue>
    {
        protected Classifier() : base(FlowControl.Continue) { }


        public virtual Dictionary<TKey, List<TValue>> Classify(IEnumerable<TValue> values)
        {
            Dictionary<TKey, List<TValue>> classify = new();

            Traverse(values, classify);

            return classify;
        }

        protected override void OnTraverse(TValue value, Dictionary<TKey, List<TValue>> context)
        {
            TKey key = GenerateKey(value, context);

            if (!context.ContainsKey(key)) context.Add(key, new() { value });
            else context[key].Add(value);
        }

        public abstract TKey GenerateKey(TValue value, Dictionary<TKey, List<TValue>> context);
    }
}
