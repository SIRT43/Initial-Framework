using UnityEngine;
using UnityEngine.Events;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>  
    /// ����ֵ�л�����������on��off״̬֮���л�������������¼���  
    /// </summary> 
    [AddComponentMenu("Initial Framework/Bool Switcher")]
    public class BoolSwitcher : MonoBehaviour
    {
        public bool isOn = false;

        [Header("Events")]
        public UnityEvent<bool> onBeforeToggle;
        public UnityEvent<bool> onAfterToggle;

        /// <summary>
        /// ��������������
        /// </summary>
        public virtual void Toggle()
        {
            onBeforeToggle?.Invoke(isOn);

            isOn = !isOn;

            onAfterToggle?.Invoke(isOn);
        }
    }
}
