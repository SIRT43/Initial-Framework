using UnityEngine;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>
    /// ʹ�����ǵ� <see cref="MonoBehaviour"/> ����������Ҫ�ڼ�� <see cref="Transform"/> �仯ʱ�Ž��и��µĹ��ܣ��Խ�ʡ���ܡ�
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
