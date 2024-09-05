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

        public virtual bool Toggle()
        {
            onBeforeToggle?.Invoke(value);

            value = !value;

            onAfterToggle?.Invoke(value);

            return value;
        }
    }
}
