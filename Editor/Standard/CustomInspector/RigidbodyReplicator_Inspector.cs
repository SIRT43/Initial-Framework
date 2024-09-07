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

            target.applyInitialForce = EditorGUILayout.Toggle("应用初始力", target.applyInitialForce);
            if (target.applyInitialForce)
            {
                target.forceRandomMode = (RandomMode)EditorGUILayout.EnumPopup("力模式", target.forceRandomMode);

                switch (target.forceRandomMode)
                {
                    case RandomMode.Fixed:
                        target.fixedForce = EditorGUILayout.Vector3Field("固定力", target.fixedForce);
                        break;
                    case RandomMode.Random:
                        EditorGUILayout.PropertyField(randomForce);
                        break;
                }
            }

            target.applyInitialVelocity = EditorGUILayout.Toggle("应用初始速度", target.applyInitialVelocity);
            if (target.applyInitialVelocity)
            {
                target.velocityRandomMode = (RandomMode)EditorGUILayout.EnumPopup("速度模式", target.velocityRandomMode);

                switch (target.velocityRandomMode)
                {
                    case RandomMode.Fixed:
                        target.fixedVelocity = EditorGUILayout.Vector3Field("固定速度", target.fixedVelocity);
                        break;
                    case RandomMode.Random:
                        EditorGUILayout.PropertyField(randomVelocity);
                        break;
                }
            }

            target.applyInitialAngularVelocity = EditorGUILayout.Toggle("应用初始角速度", target.applyInitialAngularVelocity);
            if (target.applyInitialAngularVelocity)
            {
                target.angularVelocityRandomMode = (RandomMode)EditorGUILayout.EnumPopup("角速度模式", target.angularVelocityRandomMode);

                switch (target.angularVelocityRandomMode)
                {
                    case RandomMode.Fixed:
                        target.fixedAngularVelocity = EditorGUILayout.Vector3Field("固定角速度", target.fixedAngularVelocity);
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
