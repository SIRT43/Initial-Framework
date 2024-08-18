#if UNITY_EDITOR    
using System;
using UnityEditor;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.MenuItems
{
    /// <summary>
    /// 菜单物品工具，实现了对 MenuItem 特性的便捷工具。
    /// </summary>
    public static class MenuItemUtils
    {
        /// <summary>
        /// 为 MenuCommand 上下文创建子对象并发出重命名事件。
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
