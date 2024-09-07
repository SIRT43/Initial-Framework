using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.ObjectListener
{
    /// <summary>
    /// ���ڼ��� <see cref="Collider"/> �� <see cref="Rigidbody"/> ��ͨ�û��ࡣ  
    /// 
    /// <para>
    /// ���ڴ�������� <see cref="Rigidbody"/> ������������ײ�¼������롢ͣ�����˳�����
    /// <br>�����ಢ�����ʵ������������ض�����ʱ�����޷��ṩ���ʵ���������Ż���</br>
    /// </para>
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class RigidbodyListener : MonoBehaviour
    {
        private readonly Dictionary<Collider, Rigidbody> stayingColliders = new();
        public IReadOnlyDictionary<Collider, Rigidbody> StayingColliders => stayingColliders;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody == null) return;

            stayingColliders.Add(other, other.attachedRigidbody);

            OnListened(other.attachedRigidbody, other);
            OnEnterListened(other.attachedRigidbody, other);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.attachedRigidbody == null) return;

            OnListened(other.attachedRigidbody, other);
            OnExitListened(other.attachedRigidbody, other);

            stayingColliders.Remove(other);
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            if (other.attachedRigidbody == null) return;

            OnListened(other.attachedRigidbody, other);
            OnStayListened(other.attachedRigidbody, other);
        }

        /// <summary>
        /// ���������ڴ��� <see cref="Rigidbody"/> ʱ���á�
        /// </summary>
        protected virtual void OnListened(Rigidbody rigidbody, Collider collider) { }
        /// <summary>
        /// ���������ڴ��� <see cref="Rigidbody"/> ʱ���á�
        /// </summary>
        protected virtual void OnEnterListened(Rigidbody rigidbody, Collider collider) { }
        /// <summary>
        /// ���������ڴ��� <see cref="Rigidbody"/> ʱ���á�
        /// </summary>
        protected virtual void OnExitListened(Rigidbody rigidbody, Collider collider) { }
        /// <summary>
        /// ���������ڴ��� <see cref="Rigidbody"/> ʱ���á�
        /// </summary>
        protected virtual void OnStayListened(Rigidbody rigidbody, Collider collider) { }
    }
}
