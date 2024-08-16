namespace FTGAMEStudio.InitialFramework
{
    public interface IBasicFacilityin<TKey, TRaw, out TRipe>
    {
        public void Add(TRaw raw);
        public TRipe Get(TKey key);
        public void Set(TKey key, TRaw raw);
        public void Remove(TKey key);
    }
}
