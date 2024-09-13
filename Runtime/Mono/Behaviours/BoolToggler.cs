using UnityEngine;
using UnityEngine.Events;

namespace InitialFramework
{
    /// <summary>
    /// 布尔值切换器，用于在 true 和 false 状态之间切换，并触发相关事件。
    /// </summary>
    [AddComponentMenu("Initial Framework/Bool Toggler")]
    public class BoolToggler : MonoBehaviour, IToggler
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
