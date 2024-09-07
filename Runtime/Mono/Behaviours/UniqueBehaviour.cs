using FTGAMEStudio.InitialFramework.ExtensionMethods;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>
    /// 唯一行为。
    /// <br>当您的派生树从本类开始，那么 <see cref="UniqueBehaviour"/> 会尝试阻止派生树中的每一个组件的重复实例化。</br>
    /// <br>在您 override 方法时，请使用 base 以访问基类方法，否则本类将无法正常工作。</br>
    /// 
    /// <para>
    /// 另请参阅 <seealso cref="MonoBehaviour"/>
    /// </para>
    /// </summary>
    public abstract class UniqueBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 全局组件，它用于储存全局唯一的组件。
        /// </summary>
        private static readonly Dictionary<string, UniqueBehaviour> globalComponents = new();
        /// <summary>
        /// 局部组件，它用于储存场景唯一的组件。
        /// </summary>
        private static readonly Dictionary<string, UniqueBehaviour> localCompoents = new();



        public static bool HasUnique(UniqueBehaviour unique, int buildIndex = -1)
        {
            string uniqueName = unique.GetType().GetUniqueName();

            return buildIndex == -1 ? globalComponents.ContainsKey(uniqueName) : localCompoents.ContainsKey(uniqueName);
        }



        private static bool AddUnique(UniqueBehaviour unique, int buildIndex = -1)
        {
            string uniqueName = unique.GetType().GetUniqueName();

            return buildIndex == -1 ? globalComponents.TryAdd(uniqueName, unique) : localCompoents.TryAdd(uniqueName, unique);
        }

        private static bool RemoveUnique(UniqueBehaviour unique, int buildIndex = -1)
        {
            string uniqueName = unique.GetType().GetUniqueName();

            return buildIndex == -1 ? globalComponents.Remove(uniqueName) : localCompoents.Remove(uniqueName);
        }



        [SerializeField] private bool uniqueInAllScenes = true;
        [Tooltip("当此字段为 false 时，此游戏对象将 DontDestroyOnLoad(gameObject)")]
        [SerializeField] private bool destroyOnLoad = true;


        public bool UniqueInAllScenes { get => uniqueInAllScenes; }
        public bool DestroyOnLoad { get => destroyOnLoad; }

        public int BuildIndex { get => UniqueInAllScenes ? -1 : SceneManager.GetActiveScene().buildIndex; }

        protected virtual void Awake()
        {
            if (!AddUnique(this, BuildIndex))
            {
                enabled = false;
                Debug.LogWarning($@"{GetType().GetUniqueName()} is trying to instantiate repeatedly, but it's derived from UniqueBehaviour.", this);

                return;
            }

            if (!DestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy() => RemoveUnique(this, BuildIndex);

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (!DestroyOnLoad && !UniqueInAllScenes)
            {
                uniqueInAllScenes = true;
                Debug.LogWarning("If this GameObject is dontDestroyOnLoad, then it must be unique in all scenes.", gameObject);
            }
        }
#endif
    }
}