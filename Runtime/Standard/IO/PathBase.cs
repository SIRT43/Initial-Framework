using System;
using System.IO;

namespace FTGAMEStudio.InitialFramework.IO
{
    public interface IPath
    {
        /// <summary>
        /// �ļ����ļ���������·����
        /// </summary>
        string BasePath { get; set; }
        /// <summary>
        /// �ļ������ļ�������
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// �ļ����ļ��е�����·����
        /// </summary>
        string FullPath { get; set; }

        bool Exists();

        bool Create();
        bool Delete();

        bool Move(string newBasePath);
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

        /// <summary>
        /// ��ȡ·��ָ����ļ����ļ�������
        /// </summary>
        public static string GetPathName(string path) => path[(Path.GetDirectoryName(path).Length + 1)..];



        public abstract string BasePath { get; set; }
        public abstract string Name { get; set; }

        public virtual string FullPath
        {
            get => Path.Combine(BasePath, Name);
            set
            {
                BasePath = Path.GetDirectoryName(value);
                Name = GetPathName(value);
            }
        }


        public abstract bool Exists();

        public abstract bool Create();
        public abstract bool Delete();

        public abstract bool Move(string newBasePath);


        public override string ToString() => FullPath;
    }
}
