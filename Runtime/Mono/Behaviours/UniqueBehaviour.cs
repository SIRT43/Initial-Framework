using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InitialFramework
{
    /// <summary>
    /// Ψһ��Ϊ��
    /// <br>�������������ӱ��࿪ʼ����ô <see cref="UniqueBehaviour"/> �᳢����ֹ�������е�ÿһ��������ظ�ʵ������</br>
    /// <br>���� override ����ʱ����ʹ�� base �Է��ʻ��෽���������ཫ�޷�����������</br>
    /// 
    /// <para>
    /// ������� <seealso cref="MonoBehaviour"/>
    /// </para>
    /// </summary>
    public abstract class UniqueBehaviour : MonoBehaviour
    {
        /// <summary>
        /// ȫ������������ڴ���ȫ��Ψһ�������
        /// </summary>
        private static readonly List<UniqueBehaviour> globalComponents = new();
        /// <summary>
        /// �ֲ�����������ڴ��泡��Ψһ�������
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
        [Tooltip("�����ֶ�Ϊ false ʱ������Ϸ���� DontDestroyOnLoad(gameObject)")]
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