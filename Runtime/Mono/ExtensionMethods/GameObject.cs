using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.ExtensionMethods
{
    public static class GameObjectMethods
    {
        /// <summary>
        /// 仅获取引擎组件。
        /// <br>它将仅或取此 <see cref="GameObject"/> 上的不派生自 <see cref="MonoBehaviour"/> 的组件。</br>
        /// </summary>
        public static Component[] GetEngineComponents(this GameObject gameObject)
        {
            List<Component> components = new(gameObject.GetComponents<Component>());

            components.RemoveAll(component => component is MonoBehaviour);

            return components.ToArray();
        }

        /// <summary>
        /// 仅获取单例组件。
        /// <br>它将仅或取此 <see cref="GameObject"/> 上的派生自 <see cref="MonoBehaviour"/> 的组件。</br>
        /// </summary>
        public static MonoBehaviour[] GetMonoComponents(this GameObject gameObject) =>
            gameObject.GetComponents<MonoBehaviour>();
    }
}
