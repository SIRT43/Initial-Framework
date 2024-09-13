namespace InitialFramework.IO
{
    public interface IFileReference<T> where T : FileBase
    {
        /// <summary>
        /// �Ƽ�ʹ��ֻ�������ʱ������
        /// </summary>
        UnityFile FileLocation { get; }
    }

    public interface IFileReference : IFileReference<UnityFile> { }
}
