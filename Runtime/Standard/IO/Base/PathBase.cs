using System;
using System.IO;

namespace FTGAMEStudio.InitialFramework.IO
{
    public interface IPath
    {
        /// <summary>
        /// �ļ������ļ�������
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// �ļ����ļ���������·����
        /// </summary>
        public string BasePath { get; }
        /// <summary>
        /// �ļ����ļ��е�·����
        /// </summary>
        public string FullPath { get; }

        public bool Exists();

        public bool Create();
        public bool Delete();
        public bool Move(string newPath);
    }

    /// <summary>
    /// ·�����࣬��ʾ�ļ����ļ��С�
    /// 
    /// <para>�������ȷ������Ҫָ���ļ����ļ��У��벻Ҫʹ�ñ��ࡣ</para>
    /// </summary>
    [Serializable]
    public abstract class PathBase : IPath
    {
        public static implicit operator string(PathBase path) => path.ToString();

        public abstract string Name { get; }
        public abstract string BasePath { get; }
        public virtual string FullPath => Path.Combine(BasePath, Name);

        public abstract bool Exists();

        public abstract bool Create();
        public abstract bool Delete();
        public abstract bool Move(string newPath);

        public override string ToString() => FullPath;
    }
}
