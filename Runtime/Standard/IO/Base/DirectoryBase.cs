using System.IO;

namespace InitialFramework.IO
{
    /// <summary>
    /// Ŀ¼���࣬��ʾ�ļ��С�
    /// </summary>
    public abstract class DirectoryBase : PathBase
    {
        public static implicit operator DirectoryInfo(DirectoryBase directory) => new(directory);

        public override bool Exists() => Directory.Exists(FullPath);

        /// <summary>
        /// ���ļ����Ѵ���ʱ������ false��
        /// </summary>
        public override bool Create()
        {
            if (Exists()) return false;

            Directory.CreateDirectory(FullPath);
            return true;
        }

        /// <summary>
        /// ���ļ��в�����ʱ������ false��
        /// </summary>
        public override bool Delete()
        {
            if (!Exists()) return false;

            Directory.Delete(FullPath);
            return true;
        }
    }
}
