using System;
using System.IO;

namespace FTGAMEStudio.InitialFramework.IO
{
    /// <summary>
    /// Ŀ¼���࣬��ʾ�ļ��С�
    /// </summary>
    [Serializable]
    public abstract class DirectoryBase : PathBase
    {
        public static implicit operator DirectoryInfo(DirectoryBase directory) => new(directory);

        public override bool Exists() => Directory.Exists(FullPath);

        /// <returns>���ļ����Ѵ��ڣ�ȡ������������ false��</returns>
        public override bool Create()
        {
            if (Exists()) return false;

            Directory.CreateDirectory(FullPath);
            return true;
        }

        /// <returns>���ļ��в����ڣ�ȡ������������ false��</returns>
        public override bool Delete()
        {
            if (!Exists()) return false;

            Directory.Delete(FullPath);
            return true;
        }

        /// <returns>���ļ��в����ڣ�ȡ������������ false��</returns>
        public override bool Move(string newPath)
        {
            if (!Exists()) return false;
            if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

            Directory.Move(FullPath, Path.Combine(newPath, Name));
            return true;
        }
    }
}
