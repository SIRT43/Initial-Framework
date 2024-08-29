namespace FTGAMEStudio.InitialFramework
{
    /// <summary>
    /// 可空集合，当您创建可为空的集合时，请尝试继承本接口。
    /// </summary>
    public interface IEmptyableCollection
    {
        int Count { get; }
        bool IsEmpty { get; }
    }
}
