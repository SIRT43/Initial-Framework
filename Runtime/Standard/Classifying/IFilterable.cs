namespace FTGAMEStudio.InitialFramework
{
    /// <summary>  
    /// ���˹���
    /// 
    /// <para>������� <seealso cref="IClassifiable{TKey, TValue}.IsCanonical(TValue, out TKey)"/></para>
    /// </summary>  
    public delegate bool FiltrationRule<TKey, TValue>(TValue value, out TKey key);

    public interface IFilterable<TKey, TValue>
    {
        public FiltrationRule<TKey, TValue> Filter { get; }
    }
}
