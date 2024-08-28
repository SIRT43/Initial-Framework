using UnityEngine;
using UnityEngine.Events;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>  
    /// ����ֵ�л�����������on��off״̬֮���л�������������¼���  
    /// </summary> 
    [AddComponentMenu("Initial Framework/Bool Toggle")]
    public class BoolToggle : MonoBehaviour
    {
        public bool value = false;

        [Header("Events")]
        public UnityEvent<bool> onBeforeToggle;
        public UnityEvent<bool> onAfterToggle;

        /// <summary>
        /// ��������������
        /// </summary>
        public virtual void Toggle()
        {
            onBeforeToggle?.Invoke(value);

            value = !value;

            onAfterToggle?.Invoke(value);
        }
    }
}
