using UnityEngine;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>
    /// 使用脏标记的 <see cref="MonoBehaviour"/> 它用于在需要在监控 <see cref="Transform"/> 变化时才进行更新的功能，以节省性能。
    /// </summary>
    public abstract class DirtyBehaviour : MonoBehaviour
    {
        private Vector3 oldPosition;
        private Quaternion oldRotation;

        public Vector3 DeltaPosition => transform.position - oldPosition;
        public Vector3 DeltaRotation => transform.eulerAngles - oldRotation.eulerAngles;

        public bool IsDirty
        {
            get
            {
                bool result = oldPosition != transform.position || oldRotation != transform.rotation;

                if (result)
                {
                    oldPosition = transform.position;
                    oldRotation = transform.rotation;
                }

                return result;
            }
        }

        protected virtual void Awake()
        {
            oldPosition = transform.position;
            oldRotation = transform.rotation;
        }
    }
}
