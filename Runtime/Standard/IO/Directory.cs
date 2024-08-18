using System;
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
    /// Unity ��·�������ھ������ʵĻ�·����
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
        /// ���ֶη��� <see cref="FullPath"/>��
        /// </summary>
        public override string BasePath => FullPath;
        /// <summary>
        /// ���ֶη��ؿ��ַ�����
        /// </summary>
        public override string Name => "";

        public UnityPath() => type = UnityPathType.persistentDataPath;
        public UnityPath(UnityPathType basePathType) => type = basePathType;
    }

    /// <summary>
    /// ·�������ھ������ʵ�·����
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
