using System.IO;

namespace InitialFramework.IO
{
    public interface IFile
    {
        string FileName { get; set; }
        string Extension { get; set; }

        void WriteAllText(string content);
        bool ReadAllText(out string content);
    }

    /// <summary>
    /// 文件基类，表示文件。
    /// </summary>
    public abstract class FileBase : PathBase, IFile
    {
        public static implicit operator DirectoryInfo(FileBase file) => new(file.BasePath);
        public static implicit operator FileInfo(FileBase file) => new(file.FullPath);



        public abstract string FileName { get; set; }
        public abstract string Extension { get; set; }

        public override string Name
        {
            get => $"{FileName}.{Extension}";
            set
            {
                FileName = Path.GetFileNameWithoutExtension(value);
                Extension = Path.GetExtension(value)[1..];
            }
        }



        public override bool Exists() => File.Exists(FullPath);


        /// <summary>
        /// 当文件已存在时，返回 false。
        /// </summary>
        public override bool Create()
        {
            if (Exists()) return false;

            if (!Directory.Exists(BasePath)) Directory.CreateDirectory(BasePath);

            File.Create(FullPath).Close();
            return true;
        }

        /// <summary>
        /// 当文件不存在时，返回 false。
        /// </summary>
        public override bool Delete()
        {
            if (!Exists()) return false;

            File.Delete(FullPath);
            return true;
        }


        /// <summary>
        /// 当文件不存在时，返回 false。
        /// </summary>
        public override bool Move(string newBasePath)
        {
            if (!Exists()) return false;

            File.Move(FullPath, Path.Combine(newBasePath, Name));

            BasePath = newBasePath;

            return true;
        }


        /// <summary>
        /// 文件不存在时，则创建文件。
        /// </summary>
        public void WriteAllText(string content)
        {
            if (!Exists()) Create();

            File.WriteAllText(FullPath, content);
        }

        /// <summary>
        /// 当文件不存在时，返回 false。
        /// </summary>
        public bool ReadAllText(out string content)
        {
            if (Exists())
            {
                content = File.ReadAllText(FullPath);
                return true;
            }

            content = null;
            return false;
        }



        protected FileBase(string basePath, string name) : base(basePath, name) { }
        protected FileBase(string fullPath) : base(fullPath) { }
        protected FileBase() : base() { }

        protected FileBase(string basePath, string fileName, FilenameExtension extension = FilenameExtension.infr) : this(basePath, $"{fileName}.{extension}") { }
    }
}
