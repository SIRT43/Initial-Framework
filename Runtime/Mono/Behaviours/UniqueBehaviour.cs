using FTGAMEStudio.InitialFramework.ExtensionMethods;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FTGAMEStudio.InitialFramework
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
        private static readonly Dictionary<string, UniqueBehaviour> globalComponents = new();
        /// <summary>
        /// �ֲ�����������ڴ��泡��Ψһ�������
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