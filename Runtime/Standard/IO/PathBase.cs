using System;
using System.IO;

namespace FTGAMEStudio.InitialFramework.IO
{
    public interface IPath
    {
        /// <summary>
        /// 文件或文件夹所处的路径。
        /// </summary>
        string BasePath { get; set; }
        /// <summary>
        /// 文件名或文件夹名。
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 文件或文件夹的完整路径。
        /// </summary>
        string FullPath { get; set; }

        bool Exists();

        bool Create();
        bool Delete();

        bool Move(string newBasePath);
    }

    /// <summary>
    /// 路径基类，表示文件或文件夹。
    /// 
    /// <para>如果您不确定且需要指定文件或文件夹，请不要使用本类。</para>
    /// </summary>
    [Serializable]
    public abstract class PathBase : IPath
    {
        public static implicit operator string(PathBase path) => path.ToString();

        /// <summary>
        /// 获取路径指向的文件或文件夹名。
        /// </summary>
        public static string GetPathName(string path) => path[(Path.GetDirectoryName(path).Length + 1)..];



        public abstract string BasePath { get; set; }
        public abstract string Name { get; set; }

        public virtual string FullPath
        {
            get => Path.Combine(BasePath, Name);
            set
            {
                BasePath = Path.GetDirectoryName(value);
                Name = GetPathName(value);
            }
        }


        public abstract bool Exists();

        public abstract bool Create();
        public abstract bool Delete();

        public abstract bool Move(string newBasePath);


        public override string ToString() => FullPath;
    }
}
