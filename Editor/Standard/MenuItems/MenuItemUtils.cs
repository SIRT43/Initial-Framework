#if UNITY_EDITOR    
using System;
using UnityEditor;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.MenuItems
{
    /// <summary>
    /// �˵���Ʒ���ߣ�ʵ���˶� MenuItem ���Եı�ݹ��ߡ�
    /// </summary>
    public static class MenuItemUtils
    {
        /// <summary>
        /// Ϊ MenuCommand �����Ĵ����Ӷ��󲢷����������¼���
        /// </summary>
        public static void AddChildWithMenuCommand(MenuCommand command, string gameObjectName, bool worldPositionStays = false, params Type[] components)
        {
            GameObject gameObject = new(gameObjectName, components);

            EditorUtils.SendRename(gameObject);
            Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObjectName);

            if (command.context == null) return;

            GameObject context = command.context as GameObject;
            gameObject.transform.SetParent(context.transform, worldPositionStays);
        }
    }
}
#endif
