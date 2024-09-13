using UnityEngine;
using UnityEngine.Events;

namespace InitialFramework
{
    /// <summary>
    /// ����ֵ�л����������� true �� false ״̬֮���л�������������¼���
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
