using System;
using System.IO;
using UnityEngine;

namespace InitialFramework.IO
{
    /// <summary>
    /// 文件，用于表示文件。
    /// </summary>
    [Serializable]
    public class StandardFile : FileBase
    {
        [SerializeField] private string basePath;
        [SerializeField] private string fileName;
        [SerializeField] private FilenameExtension extension;

        public override string BasePath { get => basePath; set => basePath = value; }
        public override string FileName { get => fileName; set => fileName = value; }

        public override string Extension
        {
            get => extension.ToString();
            set => extension = Enum.TryParse(value, out FilenameExtension result) ? result : FilenameExtension.infr;
        }

        public StandardFile(string basePath, string fileName, FilenameExtension extension = FilenameExtension.infr)
        {
            BasePath = basePath;
            FileName = fileName;
            Extension = extension.ToString();
        }
    }

    /// <summary>
    /// Unity 文件路径，表示基于 Unity 路径的文件。
    /// </summary>
    [Serializable]
    public class UnityFile : StandardFile
    {
        [Space]
        [SerializeField] private UnityPath unityPath;

        public override string BasePath => Path.Combine(unityPath, base.BasePath);

        public UnityFile(UnityPath unityPath, string basePath, string fileName, FilenameExtension extension = FilenameExtension.infr) :
            base(basePath, fileName, extension) => this.unityPath = unityPath;

        public UnityFile(UnityPathType unityPath, string basePath, string fileName, FilenameExtension extension = FilenameExtension.infr) :
            this(new UnityPath(unityPath), basePath, fileName, extension)
        { }

        public UnityFile(string basePath, string fileName, FilenameExtension extension = FilenameExtension.infr) :
            this(UnityPathType.persistentDataPath, basePath, fileName, extension)
        { }
    }
}
