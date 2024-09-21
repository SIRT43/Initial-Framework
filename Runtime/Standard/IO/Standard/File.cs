using System;
using System.IO;
using UnityEngine;

namespace InitialFramework.IO
{
    /// <summary>
    /// �ļ������ڱ�ʾ�ļ���
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


        public StandardFile(string fullPath) : base(fullPath) { }
        public StandardFile(string basePath, string name) : base(basePath, name) { }
        public StandardFile(string basePath, string fileName, FilenameExtension extension = FilenameExtension.infr) : base(basePath, fileName, extension) { }
    }

    /// <summary>
    /// Unity �ļ�·������ʾ���� Unity ·�����ļ���
    /// </summary>
    [Serializable]
    public class UnityFile : StandardFile
    {
        [Space]
        [SerializeField] private UnityPath unityPath;

        public override string BasePath => Path.Combine(unityPath, base.BasePath);

        public UnityFile(UnityPath unityPath, string fullPath) : base(fullPath) => this.unityPath = unityPath;
        public UnityFile(UnityPath unityPath, string basePath, string name) : base(basePath, name) => this.unityPath = unityPath;
        public UnityFile(UnityPath unityPath, string basePath, string fileName, FilenameExtension extension = FilenameExtension.infr) : base(basePath, fileName, extension) => this.unityPath = unityPath;
    }
}
