namespace FTGAMEStudio.InitialFramework.IO
{
    public interface IFileReference
    {
        /// <summary>
        /// 推荐使用只读或编译时常量。
        /// </summary>
        UnityFile FileLocation { get; }
    }
}
