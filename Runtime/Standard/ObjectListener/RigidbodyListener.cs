using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.ObjectListener
{
    /// <summary>
    /// ���ڼ��� <see cref="Collider"/> �� <see cref="Rigidbody"/> ��ͨ�û��ࡣ  
    /// 
    /// <para>
    /// �����ṩ��һ����ܣ�������Unity��Ϸ�����д�������� <see cref="Rigidbody"/> ������������ײ�¼������롢ͣ�����˳�����
    /// <br>�����ಢ�����ʵ������������ض�����ʱ�����޷��ṩ���ʵ���������Ż���</br>
    /// </para>
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class RigidbodyListener : MonoBehaviour
    {
        private readonly Dictionary<Collider, Rigidbody> rigidCaches = new();
        protected IReadOnlyDictionary<Collider, Rigidbody> RigidCaches => rigidCaches;

        protected virtual void OnTriggerEnter(Collider other)
        {
            Rigidbody rigidbody = other.attachedRigidbody;

            if (rigidbody == null) return;

            rigidCaches.Add(other, rigidbody);

            OnListened(other, rigidbody);
            OnEnterListened(other, rigidbody);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (!rigidCaches.ContainsKey(other)) return;

            OnListened(other, rigidCaches[other]);
            OnExitListened(other, rigidCaches[other]);

            rigidCaches.Remove(other);
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            if (!rigidCaches.ContainsKey(other)) return;

            OnListened(other, rigidCaches[other]);
            OnStayListened(other, rigidCaches[other]);
        }

        /// <summary>
        /// ���������ڴ��� <see cref="Rigidbody"/> ʱ���á�
        /// </summary>
        protected virtual void OnListened(Collider collider, Rigidbody rigidbody) { }
        /// <summary>
        /// ���������ڴ��� <see cref="Rigidbody"/> ʱ���á�
        /// </summary>
        protected virtual void OnEnterListened(Collider collider, Rigidbody rigidbody) { }
        /// <summary>
        /// ���������ڴ��� <see cref="Rigidbody"/> ʱ���á�
        /// </summary>
        protected virtual void OnExitListened(Collider collider, Rigidbody rigidbody) { }
        /// <summary>
        /// ���������ڴ��� <see cref="Rigidbody"/> ʱ���á�
        /// </summary>
        protected virtual void OnStayListened(Collider collider, Rigidbody rigidbody) { }
    }
}
