using System;
using System.IO;

namespace FTGAMEStudio.InitialFramework.IO
{
    public interface IFile
    {
        public string FileName { get; }
        public FilenameExtension Extension { get; }

        public void Write(string content);
        public bool Read(out string content);
    }

    /// <summary>
    /// �ļ����࣬��ʾ�ļ���
    /// </summary>
    [Serializable]
    public abstract class FileBase : PathBase, IFile
    {
        public static implicit operator DirectoryInfo(FileBase file) => new(file.BasePath);
        public static implicit operator FileInfo(FileBase file) => new(file.FullPath);

        public abstract string FileName { get; }
        public abstract FilenameExtension Extension { get; }

        public override string Name => $"{FileName}.{Extension}";

        public override bool Exists() => File.Exists(FullPath);

        /// <returns>���ļ��Ѵ��ڣ�ȡ������������ false��</returns>
        public override bool Create()
        {
            if (Exists()) return false;
            if (!Directory.Exists(BasePath)) Directory.CreateDirectory(BasePath);

            File.Create(FullPath).Close();
            return true;
        }

        /// <returns>���ļ������ڣ�ȡ������������ false��</returns>
        public override bool Delete()
        {
            if (!Exists()) return false;

            File.Delete(FullPath);
            return true;
        }

        /// <returns>���ļ������ڣ�ȡ������������ false��</returns>
        public override bool Move(string newPath)
        {
            if (!Exists()) return false;
            if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

            File.Move(FullPath, Path.Combine(newPath, Name));
            return true;
        }

        /// <summary>
        /// �ļ�������ʱ���򴴽��ļ���
        /// </summary>
        public void Write(string content)
        {
            if (!Exists()) Create();

            File.WriteAllText(FullPath, content);
        }

        /// <returns>���ļ������ڣ�ȡ������������ false��</returns>
        public bool Read(out string content)
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
