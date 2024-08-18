using System;
using System.IO;

namespace FTGAMEStudio.InitialFramework.IO
{
    /// <summary>
    /// 目录基类，表示文件夹。
    /// </summary>
    [Serializable]
    public abstract class DirectoryBase : PathBase
    {
        public static implicit operator DirectoryInfo(DirectoryBase directory) => new(directory);

        public override bool Exists() => Directory.Exists(FullPath);

        /// <returns>当文件夹已存在，取消操作并返回 false。</returns>
        public override bool Create()
        {
            if (Exists()) return false;

            Directory.CreateDirectory(FullPath);
            return true;
        }

        /// <returns>当文件夹不存在，取消操作并返回 false。</returns>
        public override bool Delete()
        {
            if (!Exists()) return false;

            Directory.Delete(FullPath);
            return true;
        }

        /// <returns>当文件夹不存在，取消操作并返回 false。</returns>
        public override bool Move(string newPath)
        {
            if (!Exists()) return false;
            if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

            Directory.Move(FullPath, Path.Combine(newPath, Name));
            return true;
        }
    }
}
