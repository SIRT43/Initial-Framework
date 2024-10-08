using System.IO;

namespace InitialFramework.IO
{
    public interface IPath
    {
        /// <summary>
        /// 文件或文件夹所处的路径。
        /// </summary>
        string BasePath { get; }
        /// <summary>
        /// 文件名或文件夹名。
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 文件或文件夹的完整路径。
        /// </summary>
        string FullPath { get; }

        bool Exists();

        bool Create();
        bool Delete();
    }

    /// <summary>
    /// 路径基类，表示文件或文件夹。
    /// 
    /// <para>如果您不确定且需要指定文件或文件夹，请不要使用本类。</para>
    /// </summary>
    public abstract class PathBase : IPath
    {
        public static implicit operator string(PathBase path) => path.ToString();

        public abstract string BasePath { get; }
        public abstract string Name { get; }

        public virtual string FullPath => Path.Combine(BasePath, Name);


        public abstract bool Exists();

        public abstract bool Create();
        public abstract bool Delete();


        public override string ToString() => FullPath;
    }
}
