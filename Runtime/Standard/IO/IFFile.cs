using System.IO;
using System.Threading.Tasks;

namespace FTGAMEStudio.InitialFramework.IO
{
    /// <summary>
    /// ���� <see cref="File"/> ��װ�Ļ����ļ������ࡣ
    /// </summary>
    public static class IFFile
    {
        /// <summary>
        /// �ж��ļ��Ƿ���ڡ�
        /// </summary>
        public static bool Exists(FilePath path) => File.Exists(path.FullName);

        /// <summary>
        /// �����ļ���
        /// 
        /// <para>���·�������ڣ����Զ�����·����
        /// <br>����ļ��Ѵ��ڣ��򴴽�ʧ�ܣ����� false��</br></para>
        /// </summary>
        public static bool Create(FilePath path, string content = null)
        {
            if (Exists(path)) return false;

            if (!path.DirectoryInfo.Exists) path.DirectoryInfo.Create();

            File.Create(path.FullName).Close();
            Overwrite(path, content);

            return true;
        }
        /// <summary>
        /// �첽�����ļ���
        /// 
        /// <para>���·�������ڣ����Զ�����·����
        /// <br>����ļ��Ѵ��ڣ��򴴽�ʧ�ܣ����� false��</br></para>
        /// </summary>
        public static async Task<bool> CreateAsync(FilePath path, string content = null)
        {
            if (Exists(path)) return false;

            if (!path.DirectoryInfo.Exists) path.DirectoryInfo.Create();

            File.Create(path.FullName).Close();
            await OverwriteAsync(path, content);

            return true;
        }

        /// <summary>
        /// ��д�ļ���
        /// <br>����ļ������ڣ���дʧ�ܣ����� false��</br>
        /// </summary>
        public static bool Overwrite(FilePath path, string content)
        {
            if (!Exists(path) || content == null) return false;

            File.WriteAllText(path.FullName, content);
            return true;
        }
        /// <summary>
        /// �첽��д�ļ���
        /// <br>����ļ������ڣ���дʧ�ܣ����� false��</br>
        /// </summary>
        public static async Task<bool> OverwriteAsync(FilePath path, string content)
        {
            if (!Exists(path) || content == null) return false;

            await File.WriteAllTextAsync(path.FullName, content);
            return true;
        }

        /// <summary>
        /// �����ļ�����д�ļ���
        /// 
        /// <para>���·�������ڣ����Զ�����·����
        /// <br>����ļ��Ѵ��ڣ���д�ļ������� true��</br></para>
        /// </summary>
        public static bool CreateOrOverwrite(FilePath path, string content)
        {
            if (!Exists(path)) return Create(path, content);
            else return Overwrite(path, content);
        }
        /// <summary>
        /// �첽�����ļ������첽��д�ļ���
        /// 
        /// <para>���·�������ڣ����Զ�����·����
        /// <br>����ļ��Ѵ��ڣ���д�ļ������� true��</br></para>
        /// </summary>
        public static async Task<bool> CreateOrOverwriteAsync(FilePath path, string content)
        {
            if (!Exists(path)) return await CreateAsync(path, content);
            else return await OverwriteAsync(path, content);
        }

        /// <summary>
        /// ��ȡ�ļ���
        /// <br>����ļ������ڣ����ȡʧ�ܣ����� null��</br>
        /// </summary>
        public static string Read(FilePath path)
        {
            if (!Exists(path)) return null;
            return File.ReadAllText(path.FullName);
        }
        /// <summary>
        /// �첽��ȡ�ļ���
        /// <br>����ļ������ڣ����ȡʧ�ܣ����� null��</br>
        /// </summary>
        public static async Task<string> ReadAsync(FilePath path)
        {
            if (!Exists(path)) return null;
            return await File.ReadAllTextAsync(path.FullName);
        }
        /// <summary>
        /// ��ȡ�ļ���
        /// <br>����ļ������ڣ����ȡʧ�ܣ����� false��</br>
        /// </summary>
        public static bool TryRead(FilePath path, out string content)
        {
            content = Read(path);
            return content != null;
        }

        /// <summary>
        /// ɾ���ļ���
        /// <br>����ļ������ڣ���ɾ��ʧ�ܣ����� false��</br>
        /// </summary>
        public static bool Delete(FilePath path)
        {
            if (!Exists(path)) return false;

            File.Delete(path.FullName);
            return true;
        }
    }
}
