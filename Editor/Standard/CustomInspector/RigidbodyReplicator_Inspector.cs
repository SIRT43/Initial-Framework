#if UNITY_EDITOR
using FTGAMEStudio.InitialFramework;
using FTGAMEStudio.InitialFramework.Replicator;
using UnityEditor;

namespace FTGAMEStudio.InitialFrameworkEditor.CustomInspector
{
    [CustomEditor(typeof(RigidbodyReplicator))]
    public class RigidbodyReplicator_Inspector : GameObjectReplicator_Inspector
    {
        protected SerializedProperty randomForce;
        protected SerializedProperty randomVelocity;
        protected SerializedProperty randomAngularVelocity;

        protected virtual void OnEnable()
        {
            randomForce = serializedObject.FindProperty("randomForce");
            randomVelocity = serializedObject.FindProperty("randomVelocity");
            randomVelocity = serializedObject.FindProperty("randomAngularVelocity");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RigidbodyReplicator target = this.target as RigidbodyReplicator;

            EditorGUILayout.Space();

            target.applyInitialForce = EditorGUILayout.Toggle("Ӧ�ó�ʼ��", target.applyInitialForce);
            if (target.applyInitialForce)
            {
                target.forceRandomMode = (RandomMode)EditorGUILayout.EnumPopup("��ģʽ", target.forceRandomMode);

                switch (target.forceRandomMode)
                {
                    case RandomMode.Fixed:
                        target.fixedForce = EditorGUILayout.Vector3Field("�̶���", target.fixedForce);
                        break;
                    case RandomMode.Random:
                        EditorGUILayout.PropertyField(randomForce);
                        break;
                }
            }

            target.applyInitialVelocity = EditorGUILayout.Toggle("Ӧ�ó�ʼ�ٶ�", target.applyInitialVelocity);
            if (target.applyInitialVelocity)
            {
                target.velocityRandomMode = (RandomMode)EditorGUILayout.EnumPopup("�ٶ�ģʽ", target.velocityRandomMode);

                switch (target.velocityRandomMode)
                {
                    case RandomMode.Fixed:
                        target.fixedVelocity = EditorGUILayout.Vector3Field("�̶��ٶ�", target.fixedVelocity);
                        break;
                    case RandomMode.Random:
                        EditorGUILayout.PropertyField(randomVelocity);
                        break;
                }
            }

            target.applyInitialAngularVelocity = EditorGUILayout.Toggle("Ӧ�ó�ʼ���ٶ�", target.applyInitialAngularVelocity);
            if (target.applyInitialAngularVelocity)
            {
                target.angularVelocityRandomMode = (RandomMode)EditorGUILayout.EnumPopup("���ٶ�ģʽ", target.angularVelocityRandomMode);

                switch (target.angularVelocityRandomMode)
                {
                    case RandomMode.Fixed:
                        target.fixedAngularVelocity = EditorGUILayout.Vector3Field("�̶����ٶ�", target.fixedAngularVelocity);
                        break;
                    case RandomMode.Random:
                        EditorGUILayout.PropertyField(randomAngularVelocity);
                        break;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif
