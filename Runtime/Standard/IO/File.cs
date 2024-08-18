using System;
using System.IO;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.IO
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
        public override FilenameExtension Extension => extension;

        public StandardFile(string basePath, string fileName, FilenameExtension extension = FilenameExtension.infr)
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

        public UnityFile(UnityPath unityPath, string basePath, string fileName, FilenameExtension extension = FilenameExtension.infr) :
            base(basePath, fileName, extension) => this.unityPath = unityPath;

        public UnityFile(UnityPathType unityPath, string basePath, string fileName, FilenameExtension extension = FilenameExtension.infr) :
            base(basePath, fileName, extension) => this.unityPath = new(unityPath);

        public UnityFile(string basePath, string fileName, FilenameExtension extension = FilenameExtension.infr) :
            this(UnityPathType.persistentDataPath, basePath, fileName, extension)
        { }
    }
}
