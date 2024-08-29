namespace FTGAMEStudio.InitialFramework
{
    /// <summary>  
    /// 过滤规则。
    /// 
    /// <para>另请参阅 <seealso cref="IClassifiable{TKey, TValue}.IsCanonical(TValue, out TKey)"/></para>
    /// </summary>  
    public delegate bool FilterRule<TKey, TValue>(TValue value, out TKey key);

    public interface IFilterable<TKey, TValue>
    {
        FilterRule<TKey, TValue> FilterRule { get; }
    }
}
