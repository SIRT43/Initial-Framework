using UnityEngine;
using UnityEngine.Events;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>  
    /// ����ֵ�л�����������on��off״̬֮���л�������������¼���  
    /// </summary> 
    [AddComponentMenu("Initial Framework/Bool Toggler")]
    public class BoolToggler : MonoBehaviour
    {
        public bool value = false;

        [Header("Events")]
        public UnityEvent<bool> onBeforeToggle;
        public UnityEvent<bool> onAfterToggle;

        public virtual bool Toggle()
        {
            onBeforeToggle?.Invoke(value);

            value = !value;

            onAfterToggle?.Invoke(value);

            return value;
        }
    }
}
