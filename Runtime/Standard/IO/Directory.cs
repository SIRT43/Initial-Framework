using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.IO
{
    public enum UnityPathType
    {
        /// <summary>
        /// 包含持久数据目录的路径。
        /// </summary>
        persistentDataPath,
        /// <summary>
        /// 控制台日志文件的路径，如果当前平台不支持日志文件，则为空字符串。
        /// </summary>
        consoleLogPath,
        /// <summary>
        /// 包含目标设备上的游戏数据文件夹路径。
        /// </summary>
        dataPath,
        /// <summary>
        /// StreamingAssets 文件夹的路径。
        /// </summary>
        streamingAssetsPath,
        /// <summary>
        /// 包含临时数据/缓存目录的路径。
        /// </summary>
        temporaryCachePath
    }

    /// <summary>
    /// Unity 基路径，用于决定访问的基路径。
    /// </summary>
    [Serializable]
    public class UnityPath : DirectoryBase
    {
        public UnityPathType type;

        public override string FullPath => type switch
        {
            UnityPathType.persistentDataPath => Application.persistentDataPath,
            UnityPathType.consoleLogPath => Application.consoleLogPath,
            UnityPathType.dataPath => Application.dataPath,
            UnityPathType.streamingAssetsPath => Application.streamingAssetsPath,
            UnityPathType.temporaryCachePath => Application.temporaryCachePath,
            _ => Application.persistentDataPath,
        };

        /// <summary>
        /// 本字段返回 <see cref="FullPath"/>。
        /// </summary>
        public override string BasePath => FullPath;
        /// <summary>
        /// 本字段返回空字符串。
        /// </summary>
        public override string Name => "";

        public UnityPath() => type = UnityPathType.persistentDataPath;
        public UnityPath(UnityPathType basePathType) => type = basePathType;
    }

    /// <summary>
    /// 路径，用于决定访问的路径。
    /// </summary>
    [Serializable]
    public class StandardPath : DirectoryBase
    {
        [SerializeField] private string basePath;
        [SerializeField] private string name;

        public override string BasePath => basePath;
        public override string Name => name;

        public StandardPath(string basePath, string name)
        {
            this.basePath = basePath;
            this.name = name;
        }
    }
}
