using UnityEngine;
using UnityEngine.Events;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>  
    /// 布尔值切换器，用于在on和off状态之间切换，并触发相关事件。  
    /// </summary> 
    [AddComponentMenu("Initial Framework/Bool Toggler")]
    public class BoolToggler : MonoBehaviour
    {
        public bool value = false;

        [Header("Events")]
        public UnityEvent<bool> onBeforeToggle;
        public UnityEvent<bool> onAfterToggle;


        public virtual void Toggle(bool value)
        {
            onBeforeToggle?.Invoke(value);

            this.value = value;

            onAfterToggle?.Invoke(value);
        }

        public virtual bool Toggle()
        {
            Toggle(!value);
            return value;
        }
    }
}
