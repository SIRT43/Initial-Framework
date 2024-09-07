#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FTGAMEStudio.InitialFrameworkEditor
{
    /// <summary>
    /// 编辑器工具，实现了更多相对于 EditorUtility 的扩展功能。
    /// </summary>
    public static class EditorUtils
    {
        /// <summary>
        /// 选择指定对象。
        /// </summary>
        public static void SelectObject(Object obj) => Selection.activeObject = obj;

        /// <summary>
        /// 获取聚焦的编辑器窗口。
        /// </summary>
        public static EditorWindow GetFocusedWindow() => EditorWindow.focusedWindow;

        /// <summary>
        /// 检查编辑器窗口。
        /// </summary>
        public static bool ExecuteWindow(string windowPath) => EditorApplication.ExecuteMenuItem("Window/" + windowPath);


        /// <summary>
        /// 向指定的编辑器窗口发送重命名事件。
        /// </summary>
        public static void SendRename(EditorWindow target) =>
            target.SendEvent(new() { keyCode = KeyCode.F2, type = EventType.KeyDown });

        /// <summary>
        /// 向聚焦的编辑器窗口发送重命名事件。
        /// </summary>
        public static void SendRename() => SendRename(GetFocusedWindow());

        /// <summary>
        /// 选择指定对象，并向聚焦的编辑器窗口发送重命名事件。
        /// </summary>
        public static void SendRename(Object obj)
        {
            SelectObject(obj);
            SendRename();
        }


        /// <summary>
        /// 通过对话框保存文件。
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="defaultName">默认文件名</param>
        /// <param name="extension">文件扩展名</param>
        /// <param name="fileContent">文件内容</param>
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
        /// 通过对话框保存文件并选择它。
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="defaultName">默认文件名</param>
        /// <param name="extension">文件扩展名</param>
        /// <param name="fileContent">文件内容</param
        public static TextAsset SaveFileAndSelect(string title, string defaultName, string extension, string message = "", string fileContent = "")
        {
            TextAsset asset = SaveFile(title, defaultName, extension, message, fileContent);
            SelectObject(asset);

            return asset;
        }
    }
}
#endif
