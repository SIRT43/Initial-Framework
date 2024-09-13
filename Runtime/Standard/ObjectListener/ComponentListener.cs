using System.Collections.Generic;
using UnityEngine;

namespace InitialFramework.ObjectListener
{
    /// <summary>
    /// 用于监听 <see cref="Rigidbody"/> 上指定派生自 <see cref="Component"/> 的组件。
    /// 
    /// <para>
    /// 用于处理与具有指定组件的物体的碰撞事件（进入、停留、退出）。
    /// <br>本基类并非最佳实践，它在面对特定需求时可能无法提供最佳实践和性能优化。</br>
    /// </para>
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class ComponentListener<TComponent> : RigidbodyListener where TComponent : Component
    {
        private readonly Dictionary<Rigidbody, TComponent> stayingComponents = new();
        public IReadOnlyDictionary<Rigidbody, TComponent> StayingComponents => stayingComponents;

        /// <summary>
        /// 本方法仅在存在 <see cref="Rigidbody"/> 时调用。
        /// 
        /// <para>当您覆写本方法时请调用基类方法，否则本类将无法正常工作。</para>
        /// </summary>
        protected override void OnEnterListened(Rigidbody rigidbody, Collider collider)
        {
            if (!rigidbody.TryGetComponent(out TComponent component)) return;

            stayingComponents.Add(rigidbody, component);

            OnListened(rigidbody, component);
            OnEnterListened(rigidbody, component);
        }

        /// <summary>
        /// 本方法仅在存在 <see cref="Rigidbody"/> 时调用。
        /// 
        /// <para>当您覆写本方法时请调用基类方法，否则本类将无法正常工作。</para>
        /// </summary>
        protected override void OnExitListened(Rigidbody rigidbody, Collider collider)
        {
            OnListened(rigidbody, StayingComponents[rigidbody]);
            OnExitListened(rigidbody, StayingComponents[rigidbody]);

            stayingComponents.Remove(rigidbody);
        }

        /// <summary>
        /// 本方法仅在存在 <see cref="Rigidbody"/> 时调用。
        /// 
        /// <para>当您覆写本方法时请调用基类方法，否则本类将无法正常工作。</para>
        /// </summary>
        protected override void OnStayListened(Rigidbody rigidbody, Collider collider)
        {
            OnListened(rigidbody, StayingComponents[rigidbody]);
            OnStayListened(rigidbody, StayingComponents[rigidbody]);
        }

        /// <summary>
        /// 本方法仅在存在 <see cref="TComponent"/> 时调用。
        /// </summary>
        protected virtual void OnListened(Rigidbody rigidbody, TComponent component) { }
        /// <summary>
        /// 本方法仅在存在 <see cref="TComponent"/> 时调用。
        /// </summary>
        protected virtual void OnEnterListened(Rigidbody rigidbody, TComponent component) { }
        /// <summary>
        /// 本方法仅在存在 <see cref="TComponent"/> 时调用。
        /// </summary>
        protected virtual void OnExitListened(Rigidbody rigidbody, TComponent component) { }
        /// <summary>
        /// 本方法仅在存在 <see cref="TComponent"/> 时调用。
        /// </summary>
        protected virtual void OnStayListened(Rigidbody rigidbody, TComponent component) { }
    }
}
