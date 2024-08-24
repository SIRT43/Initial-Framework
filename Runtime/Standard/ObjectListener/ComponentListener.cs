using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.ObjectListener
{
    /// <summary>
    /// ���ڼ��� <see cref="Collider"/> ��ָ�������� <see cref="Component"/> �������
    /// 
    /// <para>
    /// �����ṩ��һ����ܣ�������Unity��Ϸ�����д��������ָ��������������ײ�¼������롢ͣ�����˳�����
    /// <br>�����ಢ�����ʵ������������ض�����ʱ�����޷��ṩ���ʵ���������Ż���</br>
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
