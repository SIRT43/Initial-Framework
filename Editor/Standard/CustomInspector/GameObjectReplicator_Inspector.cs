#if UNITY_EDITOR
using FTGAMEStudio.InitialFramework.Replicator;
using UnityEditor;
using UnityEngine;

namespace FTGAMEStudio.InitialFrameworkEditor.CustomInspector
{
    [CustomEditor(typeof(GameObjectReplicator))]
    public class GameObjectReplicator_Inspector : Editor
    {
        public override void OnInspectorGUI()
        {
            GameObjectReplicator target = this.target as GameObjectReplicator;

            target.Original = EditorGUILayout.ObjectField("源对象", target.Original, typeof(GameObject), true) as GameObject;
            target.Parent = EditorGUILayout.ObjectField("父变换", target.Parent, typeof(Transform), true) as Transform;

            EditorGUILayout.Space();

            target.WorldPositionStays = EditorGUILayout.Toggle("保持世界坐标", target.WorldPositionStays);
            if (target.WorldPositionStays)
                target.Align = EditorGUILayout.ObjectField("对齐至", target.Align, typeof(Transform), true) as Transform;

            EditorGUILayout.Space();

            target.OverrideName = EditorGUILayout.Toggle("覆写名称", target.OverrideName);
            if (target.OverrideName)
                target.NewObjectName = EditorGUILayout.TextField("新名称", target.NewObjectName);

            EditorGUILayout.Space();

            GUILayout.Label($"已记录的复制次数: {target.ReplicateCount}");
        }
    }
}
#endif
