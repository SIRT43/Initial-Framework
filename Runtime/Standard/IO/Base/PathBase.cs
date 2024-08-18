using System;
using System.IO;

namespace FTGAMEStudio.InitialFramework.IO
{
    public interface IPath
    {
        /// <summary>
        /// 文件名或文件夹名。
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 文件或文件夹所处的路径。
        /// </summary>
        public string BasePath { get; }
        /// <summary>
        /// 文件或文件夹的路径。
        /// </summary>
        public string FullPath { get; }

        public bool Exists();

        public bool Create();
        public bool Delete();
        public bool Move(string newPath);
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

        public abstract string Name { get; }
        public abstract string BasePath { get; }
        public virtual string FullPath => Path.Combine(BasePath, Name);

        public abstract bool Exists();

        public abstract bool Create();
        public abstract bool Delete();
        public abstract bool Move(string newPath);

        public override string ToString() => FullPath;
    }
}
