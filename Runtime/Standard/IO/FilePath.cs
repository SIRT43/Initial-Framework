using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.IO
{
    public enum FilenameExtension
    {
        /// <summary>
        /// persistent 本库的默认扩展名。
        /// </summary>
        prt,
        /// <summary>
        /// 纯文本。
        /// </summary>
        txt,
        /// <summary>
        /// JavaScript Object Notation 文件。
        /// </summary>
        json,
        /// <summary>
        /// 二进制数据文件。
        /// </summary>
        bin,
        /// <summary>
        /// 二进制数据文件。
        /// </summary>
        dat,
        /// <summary>
        /// 存档文件。
        /// </summary>
        sav,
        /// <summary>
        /// 配置文件。
        /// </summary>
        cfg,
        /// <summary>
        /// 配置文件。
        /// </summary>
        set,
        /// <summary>
        /// 统计类文件。
        /// </summary>
        stats,
        /// <summary>
        /// 包类文件。
        /// </summary>
        pak,
        /// <summary>
        /// 可编程对象。
        /// </summary>
        scrobj,
        /// <summary>
        /// 游戏对象。
        /// </summary>
        gamobj
    }

    /// <summary>
    /// 基路径。
    /// </summary>
    public enum BasePath
    {
        Persistent,
        TemporaryCache,
        Data
    }

    /// <summary>
    /// 文件与路径结构。
    /// </summary>
    [Serializable]
    public struct FilePath
    {
        public static implicit operator DirectoryInfo(FilePath v) => new(v.FullPath);
        public static implicit operator FileInfo(FilePath v) => new(v.FullName);

        public static readonly Regex PathRegex = new(@"^[a-zA-Z0-9_\\\/]+$");
        public static readonly Regex NameRegex = new(@"^[a-zA-Z0-9_]+$");

        /// <summary>
        /// 获取基路径。
        /// 
        /// <para>另请参阅
        /// <br><seealso cref="Application.persistentDataPath"/></br>
        /// <br><seealso cref="Application.temporaryCachePath"/></br>
        /// <br><seealso cref="Application.dataPath"/></br>。</para>
        /// </summary>
        public static string GetBasePath(BasePath basePath) => basePath switch
        {
            BasePath.Persistent => Application.persistentDataPath,
            BasePath.TemporaryCache => Application.temporaryCachePath,
            BasePath.Data => Application.dataPath,
            _ => Application.persistentDataPath,
        };



        public BasePath basePath;

        [Space]
        public string path;
        public string name;
        public FilenameExtension extension;

        /// <summary>
        /// 获取文件名。
        /// </summary>
        public readonly string FullFile => $"{name}.{extension}";
        /// <summary>
        /// 获取文件路径。
        /// </summary>
        public readonly string FullPath => Path.Combine(GetBasePath(basePath), path);
        /// <summary>
        /// 获取全路径。
        /// 
        /// <para>另请参阅 <seealso cref="FullPath"/>，<seealso cref="FullName"/>。</para>
        /// </summary>
        public readonly string FullName => Path.Combine(FullPath, FullFile);

        public readonly FileInfo FileInfo => this;
        public readonly DirectoryInfo DirectoryInfo => this;

        public FilePath(string path, string name, FilenameExtension extension, BasePath basePath = BasePath.Persistent)
        {
            if (!PathRegex.IsMatch(path))
                throw new ArgumentException("Path contains invalid characters.", nameof(path));

            if (!NameRegex.IsMatch(name))
                throw new ArgumentException("Path contains invalid characters.", nameof(name));

            this.basePath = basePath;
            this.path = path;
            this.name = name;
            this.extension = extension;
        }
    }
}
