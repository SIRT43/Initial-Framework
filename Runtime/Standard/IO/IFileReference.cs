namespace InitialFramework.IO
{
    public interface IFileReference<T> where T : FileBase
    {
        /// <summary>
        /// 推荐使用只读或编译时常量。
        /// </summary>
        UnityFile FileLocation { get; }
    }

    public interface IFileReference : IFileReference<UnityFile> { }
}
