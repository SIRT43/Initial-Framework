using System;
using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    /// <summary>    
    /// 单可过滤分类器，它使用提供的委托来过滤和分类值。
    /// 
    /// <para>
    /// <br>这个类为那些需要灵活定义过滤逻辑的场景提供了便利。</br>
    /// <br>使用 <see cref="FilterableMonoClassifier{TKey, TValue}"/> 允许你在不修改类定义的情况下，通过传递不同的过滤规则来改变分类行为。</br>
    /// </para>
    /// </summary>    
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>    
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public class FilterableMonoClassifier<TKey, TValue> : MonoClassifier<TKey, TValue>, IMonoFilterable<TKey, TValue>
    {
        public virtual event Func<TValue, Dictionary<TKey, TValue>, TKey> KeyGenerator;
        public virtual event Func<TValue, Dictionary<TKey, TValue>, bool> ValueFilter;

        /// <summary>
        /// 在此之前，您必须完善过滤器。
        /// </summary>
        public FilterableMonoClassifier(Func<TValue, Dictionary<TKey, TValue>, TKey> keyGenerator, Func<TValue, Dictionary<TKey, TValue>, bool> valueFilter = null)
        {
            KeyGenerator = keyGenerator;
            ValueFilter = valueFilter;
        }

        public override bool IsCanonical(TValue value, Dictionary<TKey, TValue> context) => ValueFilter == null || ValueFilter.Invoke(value, context);
        public override TKey GenerateKey(TValue value, Dictionary<TKey, TValue> context) => KeyGenerator.Invoke(value, context);
    }

    /// <summary>    
    /// 多可过滤分类器，它使用提供的委托来过滤和分类值。
    /// 
    /// <para>
    /// <br>这个类为那些需要灵活定义过滤逻辑的场景提供了便利。</br>
    /// <br>使用 <see cref="FilterableMultiClassifier{TKey, TValue}"/> 允许你在不修改类定义的情况下，通过传递不同的过滤规则来改变分类行为。</br>
    /// </para>
    /// </summary>    
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>    
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public class FilterableMultiClassifier<TKey, TValue> : MultiClassifier<TKey, TValue>, IMultiFilterable<TKey, TValue>
    {
        public virtual event Func<TValue, Dictionary<TKey, List<TValue>>, TKey> KeyGenerator;
        public virtual event Func<TValue, Dictionary<TKey, List<TValue>>, bool> ValueFilter;

        /// <summary>
        /// 在此之前，您必须完善过滤器。
        /// </summary>
        public FilterableMultiClassifier(Func<TValue, Dictionary<TKey, List<TValue>>, TKey> keyGenerator, Func<TValue, Dictionary<TKey, List<TValue>>, bool> valueFilter = null)
        {
            KeyGenerator = keyGenerator;
            ValueFilter = valueFilter;
        }

        public override bool IsCanonical(TValue value, Dictionary<TKey, List<TValue>> context) => ValueFilter == null || ValueFilter.Invoke(value, context);
        public override TKey GenerateKey(TValue value, Dictionary<TKey, List<TValue>> context) => KeyGenerator.Invoke(value, context);
    }
}
