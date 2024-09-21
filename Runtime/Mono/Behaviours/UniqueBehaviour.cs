using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InitialFramework
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
        private static readonly List<UniqueBehaviour> globalComponents = new();
        /// <summary>
        /// 局部组件，它用于储存场景唯一的组件。
        /// </summary>
        private static readonly Dictionary<int, List<UniqueBehaviour>> localCompoents = new();



        public static bool HasUnique(UniqueBehaviour unique, int buildIndex)
        {
            if (buildIndex == -1) return globalComponents.Contains(unique);
            else if (localCompoents.TryGetValue(buildIndex, out List<UniqueBehaviour> uniques)) return uniques.Contains(unique);

            return false;
        }



        private static bool AddUnique(UniqueBehaviour unique, int buildIndex)
        {
            if (HasUnique(unique, buildIndex)) return false;

            if (buildIndex == -1) globalComponents.Add(unique);
            else
            {
                if (!localCompoents.ContainsKey(buildIndex)) localCompoents.Add(buildIndex, new() { unique });
                else localCompoents[buildIndex].Add(unique);
            }

            return true;
        }


        private static bool RemoveUnique(UniqueBehaviour unique, int buildIndex)
        {
            if (!HasUnique(unique, buildIndex)) return false;

            if (buildIndex == -1) return globalComponents.Remove(unique);
            else if (localCompoents.TryGetValue(buildIndex, out List<UniqueBehaviour> uniques)) return uniques.Remove(unique);

            return false;
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
                Debug.LogWarning($@"{GetType().Name} is trying to instantiate repeatedly, but it's derived from UniqueBehaviour.", this);

                return;
            }

            if (!DestroyOnLoad) DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy() => RemoveUnique(this, BuildIndex);

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (!DestroyOnLoad && !UniqueInAllScenes)
                Debug.LogWarning("If this GameObject is set to DontDestroyOnLoad, it is recommended to set UniqueInAllScenes to true to ensure uniqueness across scenes.", gameObject);
        }
#endif
    }
}