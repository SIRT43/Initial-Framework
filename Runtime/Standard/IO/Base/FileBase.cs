using System.IO;

namespace InitialFramework.IO
{
    public interface IFile
    {
        string FileName { get; }
        string Extension { get; }

        void WriteAllText(string content);
        bool ReadAllText(out string content);
    }

    /// <summary>
    /// �ļ����࣬��ʾ�ļ���
    /// </summary>
    public abstract class FileBase : PathBase, IFile
    {
        public static implicit operator DirectoryInfo(FileBase file) => new(file.BasePath);
        public static implicit operator FileInfo(FileBase file) => new(file.FullPath);


        public abstract string FileName { get; }
        public abstract string Extension { get; }

        public override string Name => $"{FileName}.{Extension}";


        public override bool Exists() => File.Exists(FullPath);

        /// <summary>
        /// ���ļ��Ѵ���ʱ������ false��
        /// </summary>
        public override bool Create()
        {
            if (Exists()) return false;

            if (!Directory.Exists(BasePath)) Directory.CreateDirectory(BasePath);

            File.Create(FullPath).Close();
            return true;
        }

        /// <summary>
        /// ���ļ�������ʱ������ false��
        /// </summary>
        public override bool Delete()
        {
            if (!Exists()) return false;

            File.Delete(FullPath);
            return true;
        }

        /// <summary>
        /// �ļ�������ʱ���򴴽��ļ���
        /// </summary>
        public void WriteAllText(string content)
        {
            if (!Exists()) Create();

            File.WriteAllText(FullPath, content);
        }

        /// <summary>
        /// ���ļ�������ʱ������ false��
        /// </summary>
        public bool ReadAllText(out string content)
        {
            if (Exists())
            {
                content = File.ReadAllText(FullPath);
                return true;
            }

            content = null;
            return false;
        }
    }
}
