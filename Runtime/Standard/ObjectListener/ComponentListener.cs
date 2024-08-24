using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.ObjectListener
{
    /// <summary>
    /// 用于监听 <see cref="Collider"/> 上指定派生自 <see cref="Component"/> 的组件。
    /// 
    /// <para>
    /// 此类提供了一个框架，用于在Unity游戏开发中处理与具有指定组件的物体的碰撞事件（进入、停留、退出）。
    /// <br>本基类并非最佳实践，它在面对特定需求时可能无法提供最佳实践和性能优化。</br>
    /// </para>
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class ComponentListener<TComponent> : RigidbodyListener where TComponent : Component
    {
        private readonly Dictionary<Collider, TComponent> compCaches = new();
        protected IReadOnlyDictionary<Collider, TComponent> CompCaches => compCaches;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            TComponent component = RigidCaches.ContainsKey(other) ? RigidCaches[other].GetComponent<TComponent>() : other.GetComponent<TComponent>();

            compCaches.Add(other, component);

            OnListened(other, component);
            OnEnterListened(other, component);
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);

            if (!compCaches.ContainsKey(other)) return;

            OnListened(other, compCaches[other]);
            OnExitListened(other, compCaches[other]);

            compCaches.Remove(other);
        }

        protected override void OnTriggerStay(Collider other)
        {
            base.OnTriggerStay(other);

            OnListened(other, compCaches[other]);
            OnStayListened(other, compCaches[other]);
        }


        protected virtual void OnListened(Collider collider, TComponent component) { }
        protected virtual void OnEnterListened(Collider collider, TComponent component) { }
        protected virtual void OnExitListened(Collider collider, TComponent component) { }
        protected virtual void OnStayListened(Collider collider, TComponent component) { }
    }
}
