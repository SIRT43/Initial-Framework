using System.IO;

namespace InitialFramework.IO
{
    /// <summary>
    /// 目录基类，表示文件夹。
    /// </summary>
    public abstract class DirectoryBase : PathBase
    {
        public static implicit operator DirectoryInfo(DirectoryBase directory) => new(directory);



        public override bool Exists() => Directory.Exists(FullPath);


        /// <summary>
        /// 当文件夹已存在时，返回 false。
        /// </summary>
        public override bool Create()
        {
            if (Exists()) return false;

            Directory.CreateDirectory(FullPath);
            return true;
        }

        /// <summary>
        /// 当文件夹不存在时，返回 false。
        /// </summary>
        public override bool Delete()
        {
            if (!Exists()) return false;

            Directory.Delete(FullPath);
            return true;
        }


        /// <summary>
        /// 当文件夹不存在时，返回 false。
        /// </summary>
        public override bool Move(string newBasePath)
        {
            if (!Exists()) return false;

            Directory.Move(FullPath, Path.Combine(newBasePath, Name));

            BasePath = newBasePath;

            return true;
        }



        protected DirectoryBase(string basePath, string name) : base(basePath, name) { }
        protected DirectoryBase(string fullPath) : base(fullPath) { }
        protected DirectoryBase() : base() { }
    }
}
