using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.ExtensionMethods
{
    public static class GameObjectMethods
    {
        /// <summary>
        /// ����ȡ���������
        /// <br>��������ȡ�� <see cref="GameObject"/> �ϵĲ������� <see cref="MonoBehaviour"/> �������</br>
        /// </summary>
        public static Component[] GetEngineComponents(this GameObject gameObject)
        {
            List<Component> components = new(gameObject.GetComponents<Component>());

            components.RemoveAll(component => component is MonoBehaviour);

            return components.ToArray();
        }

        /// <summary>
        /// ����ȡ���������
        /// <br>��������ȡ�� <see cref="GameObject"/> �ϵ������� <see cref="MonoBehaviour"/> �������</br>
        /// </summary>
        public static MonoBehaviour[] GetMonoComponents(this GameObject gameObject) =>
            gameObject.GetComponents<MonoBehaviour>();
    }
}
