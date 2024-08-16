using System.IO;
using System.Threading.Tasks;

namespace FTGAMEStudio.InitialFramework.IO
{
    /// <summary>
    /// 基于 <see cref="File"/> 封装的基础文件操作类。
    /// </summary>
    public static class IFFile
    {
        /// <summary>
        /// 判断文件是否存在。
        /// </summary>
        public static bool Exists(FilePath path) => File.Exists(path.FullName);

        /// <summary>
        /// 创建文件。
        /// 
        /// <para>如果路径不存在，将自动创建路径。
        /// <br>如果文件已存在，则创建失败，返回 false。</br></para>
        /// </summary>
        public static bool Create(FilePath path, string content = null)
        {
            if (Exists(path)) return false;

            if (!path.DirectoryInfo.Exists) path.DirectoryInfo.Create();

            File.Create(path.FullName).Close();
            Overwrite(path, content);

            return true;
        }
        /// <summary>
        /// 异步创建文件。
        /// 
        /// <para>如果路径不存在，将自动创建路径。
        /// <br>如果文件已存在，则创建失败，返回 false。</br></para>
        /// </summary>
        public static async Task<bool> CreateAsync(FilePath path, string content = null)
        {
            if (Exists(path)) return false;

            if (!path.DirectoryInfo.Exists) path.DirectoryInfo.Create();

            File.Create(path.FullName).Close();
            await OverwriteAsync(path, content);

            return true;
        }

        /// <summary>
        /// 覆写文件。
        /// <br>如果文件不存在，则覆写失败，返回 false。</br>
        /// </summary>
        public static bool Overwrite(FilePath path, string content)
        {
            if (!Exists(path) || content == null) return false;

            File.WriteAllText(path.FullName, content);
            return true;
        }
        /// <summary>
        /// 异步覆写文件。
        /// <br>如果文件不存在，则覆写失败，返回 false。</br>
        /// </summary>
        public static async Task<bool> OverwriteAsync(FilePath path, string content)
        {
            if (!Exists(path) || content == null) return false;

            await File.WriteAllTextAsync(path.FullName, content);
            return true;
        }

        /// <summary>
        /// 创建文件，或覆写文件。
        /// 
        /// <para>如果路径不存在，将自动创建路径。
        /// <br>如果文件已存在，覆写文件，返回 true。</br></para>
        /// </summary>
        public static bool CreateOrOverwrite(FilePath path, string content)
        {
            if (!Exists(path)) return Create(path, content);
            else return Overwrite(path, content);
        }
        /// <summary>
        /// 异步创建文件，或异步覆写文件。
        /// 
        /// <para>如果路径不存在，将自动创建路径。
        /// <br>如果文件已存在，覆写文件，返回 true。</br></para>
        /// </summary>
        public static async Task<bool> CreateOrOverwriteAsync(FilePath path, string content)
        {
            if (!Exists(path)) return await CreateAsync(path, content);
            else return await OverwriteAsync(path, content);
        }

        /// <summary>
        /// 读取文件。
        /// <br>如果文件不存在，则读取失败，返回 null。</br>
        /// </summary>
        public static string Read(FilePath path)
        {
            if (!Exists(path)) return null;
            return File.ReadAllText(path.FullName);
        }
        /// <summary>
        /// 异步读取文件。
        /// <br>如果文件不存在，则读取失败，返回 null。</br>
        /// </summary>
        public static async Task<string> ReadAsync(FilePath path)
        {
            if (!Exists(path)) return null;
            return await File.ReadAllTextAsync(path.FullName);
        }
        /// <summary>
        /// 读取文件。
        /// <br>如果文件不存在，则读取失败，返回 false。</br>
        /// </summary>
        public static bool TryRead(FilePath path, out string content)
        {
            content = Read(path);
            return content != null;
        }

        /// <summary>
        /// 删除文件。
        /// <br>如果文件不存在，则删除失败，返回 false。</br>
        /// </summary>
        public static bool Delete(FilePath path)
        {
            if (!Exists(path)) return false;

            File.Delete(path.FullName);
            return true;
        }
    }
}
