namespace InitialFramework.Classifying
{
    /// <summary>  
    /// 过滤规则。
    /// 
    /// <para>另请参阅 <seealso cref="IFilterable{TKey, TValue}"/>，<seealso cref="FilterableClassifier{TKey, TValue}"/>。</para>
    /// </summary>  
    public delegate bool ValueFilter<in TValue>(TValue value);
    public delegate TKey KeyGenerator<out TKey, in TValue>(TValue value);

    public interface IFilterable<TKey, TValue>
    {
        event ValueFilter<TValue> ValueFilter;
        event KeyGenerator<TKey, TValue> KeyGenerator;
    }
}
