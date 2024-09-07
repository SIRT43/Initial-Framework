#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FTGAMEStudio.InitialFrameworkEditor
{
    /// <summary>
    /// �༭�����ߣ�ʵ���˸�������� EditorUtility ����չ���ܡ�
    /// </summary>
    public static class EditorUtils
    {
        /// <summary>
        /// ѡ��ָ������
        /// </summary>
        public static void SelectObject(Object obj) => Selection.activeObject = obj;

        /// <summary>
        /// ��ȡ�۽��ı༭�����ڡ�
        /// </summary>
        public static EditorWindow GetFocusedWindow() => EditorWindow.focusedWindow;

        /// <summary>
        /// ���༭�����ڡ�
        /// </summary>
        public static bool ExecuteWindow(string windowPath) => EditorApplication.ExecuteMenuItem("Window/" + windowPath);


        /// <summary>
        /// ��ָ���ı༭�����ڷ����������¼���
        /// </summary>
        public static void SendRename(EditorWindow target) =>
            target.SendEvent(new() { keyCode = KeyCode.F2, type = EventType.KeyDown });

        /// <summary>
        /// ��۽��ı༭�����ڷ����������¼���
        /// </summary>
        public static void SendRename() => SendRename(GetFocusedWindow());

        /// <summary>
        /// ѡ��ָ�����󣬲���۽��ı༭�����ڷ����������¼���
        /// </summary>
        public static void SendRename(Object obj)
        {
            SelectObject(obj);
            SendRename();
        }


        /// <summary>
        /// ͨ���Ի��򱣴��ļ���
        /// </summary>
        /// <param name="title">�Ի������</param>
        /// <param name="defaultName">Ĭ���ļ���</param>
        /// <param name="extension">�ļ���չ��</param>
        /// <param name="fileContent">�ļ�����</param>
        public static TextAsset SaveFile(string title, string defaultName, string extension, string message = "", string fileContent = "")
        {
            string path = EditorUtility.SaveFilePanelInProject(title, defaultName, extension, message);

            if (path == null || path == "") return null;

            File.Create(path).Close();
            if (!string.IsNullOrEmpty(fileContent)) File.WriteAllText(path, fileContent);

            AssetDatabase.Refresh();
             
            return AssetDatabase.LoadAssetAtPath<TextAsset>(path);
        }

        /// <summary>
        /// ͨ���Ի��򱣴��ļ���ѡ������
        /// </summary>
        /// <param name="title">�Ի������</param>
        /// <param name="defaultName">Ĭ���ļ���</param>
        /// <param name="extension">�ļ���չ��</param>
        /// <param name="fileContent">�ļ�����</param
        public static TextAsset SaveFileAndSelect(string title, string defaultName, string extension, string message = "", string fileContent = "")
        {
            TextAsset asset = SaveFile(title, defaultName, extension, message, fileContent);
            SelectObject(asset);

            return asset;
        }
    }
}
#endif
