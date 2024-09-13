using System.Collections.Generic;
using UnityEngine;

namespace InitialFramework.ObjectListener
{
    /// <summary>
    /// ���ڼ��� <see cref="Rigidbody"/> ��ָ�������� <see cref="Component"/> �������
    /// 
    /// <para>
    /// ���ڴ��������ָ��������������ײ�¼������롢ͣ�����˳�����
    /// <br>�����ಢ�����ʵ������������ض�����ʱ�����޷��ṩ���ʵ���������Ż���</br>
    /// </para>
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class ComponentListener<TComponent> : RigidbodyListener where TComponent : Component
    {
        private readonly Dictionary<Rigidbody, TComponent> stayingComponents = new();
        public IReadOnlyDictionary<Rigidbody, TComponent> StayingComponents => stayingComponents;

        /// <summary>
        /// ���������ڴ��� <see cref="Rigidbody"/> ʱ���á�
        /// 
        /// <para>������д������ʱ����û��෽���������ཫ�޷�����������</para>
        /// </summary>
        protected override void OnEnterListened(Rigidbody rigidbody, Collider collider)
        {
            if (!rigidbody.TryGetComponent(out TComponent component)) return;

            stayingComponents.Add(rigidbody, component);

            OnListened(rigidbody, component);
            OnEnterListened(rigidbody, component);
        }

        /// <summary>
        /// ���������ڴ��� <see cref="Rigidbody"/> ʱ���á�
        /// 
        /// <para>������д������ʱ����û��෽���������ཫ�޷�����������</para>
        /// </summary>
        protected override void OnExitListened(Rigidbody rigidbody, Collider collider)
        {
            OnListened(rigidbody, StayingComponents[rigidbody]);
            OnExitListened(rigidbody, StayingComponents[rigidbody]);

            stayingComponents.Remove(rigidbody);
        }

        /// <summary>
        /// ���������ڴ��� <see cref="Rigidbody"/> ʱ���á�
        /// 
        /// <para>������д������ʱ����û��෽���������ཫ�޷�����������</para>
        /// </summary>
        protected override void OnStayListened(Rigidbody rigidbody, Collider collider)
        {
            OnListened(rigidbody, StayingComponents[rigidbody]);
            OnStayListened(rigidbody, StayingComponents[rigidbody]);
        }

        /// <summary>
        /// ���������ڴ��� <see cref="TComponent"/> ʱ���á�
        /// </summary>
        protected virtual void OnListened(Rigidbody rigidbody, TComponent component) { }
        /// <summary>
        /// ���������ڴ��� <see cref="TComponent"/> ʱ���á�
        /// </summary>
        protected virtual void OnEnterListened(Rigidbody rigidbody, TComponent component) { }
        /// <summary>
        /// ���������ڴ��� <see cref="TComponent"/> ʱ���á�
        /// </summary>
        protected virtual void OnExitListened(Rigidbody rigidbody, TComponent component) { }
        /// <summary>
        /// ���������ڴ��� <see cref="TComponent"/> ʱ���á�
        /// </summary>
        protected virtual void OnStayListened(Rigidbody rigidbody, TComponent component) { }
    }
}
