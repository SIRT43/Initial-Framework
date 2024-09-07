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

            target.Original = EditorGUILayout.ObjectField("Դ����", target.Original, typeof(GameObject), true) as GameObject;
            target.Parent = EditorGUILayout.ObjectField("���任", target.Parent, typeof(Transform), true) as Transform;

            EditorGUILayout.Space();

            target.WorldPositionStays = EditorGUILayout.Toggle("������������", target.WorldPositionStays);
            if (target.WorldPositionStays)
                target.Align = EditorGUILayout.ObjectField("������", target.Align, typeof(Transform), true) as Transform;

            EditorGUILayout.Space();

            target.OverrideName = EditorGUILayout.Toggle("��д����", target.OverrideName);
            if (target.OverrideName)
                target.NewObjectName = EditorGUILayout.TextField("������", target.NewObjectName);

            EditorGUILayout.Space();

            GUILayout.Label($"�Ѽ�¼�ĸ��ƴ���: {target.ReplicateCount}");
        }
    }
}
#endif
