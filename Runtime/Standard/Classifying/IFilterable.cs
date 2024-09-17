using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    /// <summary>  
    /// 过滤规则。
    /// 
    /// <para>另请参阅 <seealso cref="IFilterable{TKey, TValue}"/>，<seealso cref="FilterableClassifier{TKey, TValue}"/>。</para>
    /// </summary>  
    public delegate bool ValueFilter<TKey, TValue>(TValue value, Dictionary<TKey, List<TValue>> context);
    public delegate TKey KeyGenerator<TKey, TValue>(TValue value, Dictionary<TKey, List<TValue>> context);

    public interface IFilterable<TKey, TValue>
    {
        event ValueFilter<TKey, TValue> ValueFilter;
        event KeyGenerator<TKey, TValue> KeyGenerator;
    }
}
