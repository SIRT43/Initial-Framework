namespace FTGAMEStudio.InitialFramework
{
    /// <summary>
    /// 可空集合，当您创建可为空的集合时，请尝试继承本接口。
    /// </summary>
    public interface IEmptyableCollection
    {
        public int Count { get; }
        public bool IsEmpty { get; }
    }
}
