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

        public override string BasePath => basePath;
        public override string FileName => fileName;

        public override string Extension => extension.ToString();

        public StandardFile(string basePath, string fileName, FilenameExtension extension)
        {
            this.basePath = basePath;
            this.fileName = fileName;
            this.extension = extension;
        }
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

        public UnityFile(UnityPath unityPath, string basePath, string fileName, FilenameExtension extension) : base(basePath, fileName, extension) => this.unityPath = unityPath;
        public UnityFile(UnityPathType unityPath, string basePath, string fileName, FilenameExtension extension) : this(new UnityPath(unityPath), basePath, fileName, extension) { }
    }
}
