#if UNITY_EDITOR  
using UnityEditor;

namespace InitialFrameworkEditor.MenuItems
{
    /// <summary>
    /// 为创建菜单添加更多文件类型。
    /// </summary>
    public static class CreateFiles
    {
        [MenuItem("Assets/Create/File/Text", priority = 19)]
        private static void Text() =>
            EditorUtils.SaveFileAndSelect("创建新文件", "新建文本文档", "txt");

        [MenuItem("Assets/Create/File/Markdown", priority = 30)]
        private static void Markdown() =>
            EditorUtils.SaveFileAndSelect("创建新文件", "新建 Markdown", "md");

        [MenuItem("Assets/Create/File/JavaScript Object Notation", priority = 41)]
        private static void JavaScriptObjectNotation() =>
            EditorUtils.SaveFileAndSelect("创建新文件", "新建 JavaScript Object Notation", "json");

        [MenuItem("Assets/Create/File/XML", priority = 42)]
        private static void XML() =>
            EditorUtils.SaveFileAndSelect("创建新文件", "新建 XML", "xml");

        [MenuItem("Assets/Create/File/C# Script (Empty)", priority = 53)]
        private static void EmptyCSharp() =>
            EditorUtils.SaveFileAndSelect("创建新文件", "New Empty C# Script", "cs");

        [MenuItem("Assets/Create/File/Shader (Empty)", priority = 54)]
        private static void EmptyShader() =>
            EditorUtils.SaveFileAndSelect("创建新文件", "New Empty Shader", "shader", fileContent: @"Shader ""NewShader""
        {
            Properties
            {

            }
    SubShader
            {
                Pass
                {
                    CGPROGRAM

                    ENDCG
                }
            }
        }
");
    }
}
#endif