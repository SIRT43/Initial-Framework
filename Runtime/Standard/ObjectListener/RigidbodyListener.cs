using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.ObjectListener
{
    /// <summary>
    /// 用于监听 <see cref="Collider"/> 的 <see cref="Rigidbody"/> 的通用基类。  
    /// 
    /// <para>
    /// 用于处理与具有 <see cref="Rigidbody"/> 组件的物体的碰撞事件（进入、停留、退出）。
    /// <br>本基类并非最佳实践，它在面对特定需求时可能无法提供最佳实践和性能优化。</br>
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
        /// 本方法仅在存在 <see cref="Rigidbody"/> 时调用。
        /// </summary>
        protected virtual void OnListened(Rigidbody rigidbody, Collider collider) { }
        /// <summary>
        /// 本方法仅在存在 <see cref="Rigidbody"/> 时调用。
        /// </summary>
        protected virtual void OnEnterListened(Rigidbody rigidbody, Collider collider) { }
        /// <summary>
        /// 本方法仅在存在 <see cref="Rigidbody"/> 时调用。
        /// </summary>
        protected virtual void OnExitListened(Rigidbody rigidbody, Collider collider) { }
        /// <summary>
        /// 本方法仅在存在 <see cref="Rigidbody"/> 时调用。
        /// </summary>
        protected virtual void OnStayListened(Rigidbody rigidbody, Collider collider) { }
    }
}
