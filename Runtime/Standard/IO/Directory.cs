using System;
using System.IO;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.IO
{
    public enum UnityPathType
    {
        /// <summary>
        /// �����־�����Ŀ¼��·����
        /// </summary>
        persistentDataPath,
        /// <summary>
        /// ����̨��־�ļ���·���������ǰƽ̨��֧����־�ļ�����Ϊ���ַ�����
        /// </summary>
        consoleLogPath,
        /// <summary>
        /// ����Ŀ���豸�ϵ���Ϸ�����ļ���·����
        /// </summary>
        dataPath,
        /// <summary>
        /// StreamingAssets �ļ��е�·����
        /// </summary>
        streamingAssetsPath,
        /// <summary>
        /// ������ʱ����/����Ŀ¼��·����
        /// </summary>
        temporaryCachePath
    }

    /// <summary>
    /// ·�������ھ������ʵ�·����
    /// </summary>
    [Serializable]
    public class StandardPath : DirectoryBase
    {
        [SerializeField] private string basePath;
        [SerializeField] private string name;

        public override string BasePath { get => basePath; set => basePath = value; }
        public override string Name { get => name; set => name = value; }

        public StandardPath(string basePath, string name)
        {
            BasePath = basePath;
            Name = name;
        }
    }

    /// <summary>
    /// Unity ��·�������ھ������ʵĻ�·����
    /// </summary>
    [Serializable]
    public class UnityPath : DirectoryBase
    {
        public UnityPathType type;

        /// <summary>
        /// ���޷����и�ֵ������
        /// </summary>
        public override string FullPath
        {
            get => type switch
            {
                UnityPathType.persistentDataPath => Application.persistentDataPath,
                UnityPathType.consoleLogPath => Application.consoleLogPath,
                UnityPathType.dataPath => Application.dataPath,
                UnityPathType.streamingAssetsPath => Application.streamingAssetsPath,
                UnityPathType.temporaryCachePath => Application.temporaryCachePath,
                _ => Application.persistentDataPath,
            };
            set => throw new InvalidOperationException("You can't modify the Unity path.");
        }

        /// <summary>
        /// ���޷����и�ֵ������
        /// </summary>
        public override string BasePath { get => Path.GetDirectoryName(FullPath); set => throw new InvalidOperationException("You can't modify the Unity path."); }
        /// <summary>
        /// ���޷����и�ֵ������
        /// </summary>
        public override string Name { get => GetPathName(FullPath); set => throw new InvalidOperationException("You can't modify the Unity path."); }

        /// <summary>
        /// ��������Զ���� true��
        /// </summary>
        public override bool Exists() => true;
        /// <summary>
        /// ���޷����д˲�����
        /// </summary>
        public override bool Create() => throw new InvalidOperationException("You can't modify the Unity path.");
        /// <summary>
        /// ���޷����д˲�����
        /// </summary>
        public override bool Delete() => throw new InvalidOperationException("You can't modify the Unity path.");
        /// <summary>
        /// ���޷����д˲�����
        /// </summary>
        public override bool Move(string newBasePath) => throw new InvalidOperationException("You can't modify the Unity path.");

        public UnityPath(UnityPathType type) => this.type = type;
    }
}
