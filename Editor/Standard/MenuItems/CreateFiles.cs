#if UNITY_EDITOR  
using UnityEditor;

namespace InitialFrameworkEditor.MenuItems
{
    /// <summary>
    /// Ϊ�����˵���Ӹ����ļ����͡�
    /// </summary>
    public static class CreateFiles
    {
        [MenuItem("Assets/Create/File/Text", priority = 19)]
        private static void Text() =>
            EditorUtils.SaveFileAndSelect("�������ļ�", "�½��ı��ĵ�", "txt");

        [MenuItem("Assets/Create/File/Markdown", priority = 30)]
        private static void Markdown() =>
            EditorUtils.SaveFileAndSelect("�������ļ�", "�½� Markdown", "md");

        [MenuItem("Assets/Create/File/JavaScript Object Notation", priority = 41)]
        private static void JavaScriptObjectNotation() =>
            EditorUtils.SaveFileAndSelect("�������ļ�", "�½� JavaScript Object Notation", "json");

        [MenuItem("Assets/Create/File/XML", priority = 42)]
        private static void XML() =>
            EditorUtils.SaveFileAndSelect("�������ļ�", "�½� XML", "xml");

        [MenuItem("Assets/Create/File/C# Script (Empty)", priority = 53)]
        private static void EmptyCSharp() =>
            EditorUtils.SaveFileAndSelect("�������ļ�", "New Empty C# Script", "cs");

        [MenuItem("Assets/Create/File/Shader (Empty)", priority = 54)]
        private static void EmptyShader() =>
            EditorUtils.SaveFileAndSelect("�������ļ�", "New Empty Shader", "shader", fileContent: @"Shader ""NewShader""
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