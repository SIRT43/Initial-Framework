using System;
using System.IO;

namespace FTGAMEStudio.InitialFramework.IO
{
    public interface IPath
    {
        /// <summary>
        /// �ļ������ļ�������
        /// </summary>
        string Name { get; }
        /// <summary>
        /// �ļ����ļ���������·����
        /// </summary>
        string BasePath { get; }
        /// <summary>
        /// �ļ����ļ��е�����·����
        /// </summary>
        string FullPath { get; }

        bool Exists();

        bool Create();
        bool Delete();
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

        public override string ToString() => FullPath;
    }
}
