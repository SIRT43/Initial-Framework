using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.ObjectListener
{
    /// <summary>
    /// 用于监听 <see cref="Collider"/> 的 <see cref="Rigidbody"/> 的通用基类。  
    /// 
    /// <para>
    /// 此类提供了一个框架，用于在Unity游戏开发中处理与具有 <see cref="Rigidbody"/> 组件的物体的碰撞事件（进入、停留、退出）。
    /// <br>本基类并非最佳实践，它在面对特定需求时可能无法提供最佳实践和性能优化。</br>
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
        /// 本方法仅在存在 <see cref="Rigidbody"/> 时调用。
        /// </summary>
        protected virtual void OnListened(Collider collider, Rigidbody rigidbody) { }
        /// <summary>
        /// 本方法仅在存在 <see cref="Rigidbody"/> 时调用。
        /// </summary>
        protected virtual void OnEnterListened(Collider collider, Rigidbody rigidbody) { }
        /// <summary>
        /// 本方法仅在存在 <see cref="Rigidbody"/> 时调用。
        /// </summary>
        protected virtual void OnExitListened(Collider collider, Rigidbody rigidbody) { }
        /// <summary>
        /// 本方法仅在存在 <see cref="Rigidbody"/> 时调用。
        /// </summary>
        protected virtual void OnStayListened(Collider collider, Rigidbody rigidbody) { }
    }
}
