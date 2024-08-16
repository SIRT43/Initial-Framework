using UnityEngine;
using UnityEngine.Events;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>  
    /// 布尔值切换器，用于在on和off状态之间切换，并触发相关事件。  
    /// </summary> 
    [AddComponentMenu("Initial Framework/Bool Switcher")]
    public class BoolSwitcher : MonoBehaviour
    {
        public bool isOn = false;

        [Header("Events")]
        public UnityEvent<bool> onBeforeToggle;
        public UnityEvent<bool> onAfterToggle;

        /// <summary>
        /// 触发本交换器。
        /// </summary>
        public virtual void Toggle()
        {
            onBeforeToggle?.Invoke(isOn);

            isOn = !isOn;

            onAfterToggle?.Invoke(isOn);
        }
    }
}
