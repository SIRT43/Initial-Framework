using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.IO
{
    public enum FilenameExtension
    {
        /// <summary>
        /// persistent �����Ĭ����չ����
        /// </summary>
        prt,
        /// <summary>
        /// ���ı���
        /// </summary>
        txt,
        /// <summary>
        /// JavaScript Object Notation �ļ���
        /// </summary>
        json,
        /// <summary>
        /// �����������ļ���
        /// </summary>
        bin,
        /// <summary>
        /// �����������ļ���
        /// </summary>
        dat,
        /// <summary>
        /// �浵�ļ���
        /// </summary>
        sav,
        /// <summary>
        /// �����ļ���
        /// </summary>
        cfg,
        /// <summary>
        /// �����ļ���
        /// </summary>
        set,
        /// <summary>
        /// ͳ�����ļ���
        /// </summary>
        stats,
        /// <summary>
        /// �����ļ���
        /// </summary>
        pak,
        /// <summary>
        /// �ɱ�̶���
        /// </summary>
        scrobj,
        /// <summary>
        /// ��Ϸ����
        /// </summary>
        gamobj
    }

    /// <summary>
    /// ��·����
    /// </summary>
    public enum BasePath
    {
        Persistent,
        TemporaryCache,
        Data
    }

    /// <summary>
    /// �ļ���·���ṹ��
    /// </summary>
    [Serializable]
    public struct FilePath
    {
        public static implicit operator DirectoryInfo(FilePath v) => new(v.FullPath);
        public static implicit operator FileInfo(FilePath v) => new(v.FullName);

        public static readonly Regex PathRegex = new(@"^[a-zA-Z0-9_\\\/]+$");
        public static readonly Regex NameRegex = new(@"^[a-zA-Z0-9_]+$");

        /// <summary>
        /// ��ȡ��·����
        /// 
        /// <para>�������
        /// <br><seealso cref="Application.persistentDataPath"/></br>
        /// <br><seealso cref="Application.temporaryCachePath"/></br>
        /// <br><seealso cref="Application.dataPath"/></br>��</para>
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
        /// ��ȡ�ļ�����
        /// </summary>
        public readonly string FullFile => $"{name}.{extension}";
        /// <summary>
        /// ��ȡ�ļ�·����
        /// </summary>
        public readonly string FullPath => Path.Combine(GetBasePath(basePath), path);
        /// <summary>
        /// ��ȡȫ·����
        /// 
        /// <para>������� <seealso cref="FullPath"/>��<seealso cref="FullName"/>��</para>
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
