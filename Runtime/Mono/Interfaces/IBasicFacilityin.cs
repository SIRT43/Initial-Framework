namespace FTGAMEStudio.InitialFramework
{
    public interface IBasicFacilityin<TKey, TRaw, out TRipe>
    {
        void Add(TRaw raw);
        TRipe Get(TKey key);
        void Set(TKey key, TRaw raw);
        void Remove(TKey key);
    }
}
