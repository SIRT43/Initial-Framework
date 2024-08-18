using System;
using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Collections.Generic
{
    /// <summary>
    /// ���������ܹ���Ķ���ʱ����̳б��ӿڡ�
    /// <br>������� <seealso cref="OrganizeBase{TDictionary, TValue}"/></br>
    /// </summary>
    public interface IOrganized
    {
        /// <summary>
        /// ������� <see cref="System.Guid"/>��
        /// <br>���û������Ҫ����Ӧ�ý����ֶγ�ʼ��Ϊ����ʱ������</br>
        /// </summary>
        public Guid Guid { get; }
    }

    public interface IOrganizer<T> where T : IOrganized
    {
        /// <summary>
        /// ע��ֵ��ֵ�� <see cref="Guid"/> Ӧ����Ψһ�ġ�
        /// </summary>
        public bool RegValue(T value);

        public T GetValue(Guid guid);
        public bool TryGetValue(Guid guid, out T value);

        public bool HasReg(Guid guid);

        public bool Override(T value);

        public bool RemoveValue(Guid guid);
    }

    /// <summary>
    /// ������һ��������࣬���ڹ���ʵ���� <see cref="IOrganized"/> �ӿڵĶ���
    /// 
    /// <para>������� <seealso cref="IOrganized"/>��<seealso cref="IOrganizer{T}"/></para>
    /// </summary>
    public abstract class Organize<TValue> : IOrganizer<TValue> where TValue : IOrganized
    {
        /// <summary>
        /// ע���
        /// <br>�����û������Ҫ�������ֵ�ע�����</br>
        /// </summary>
        protected SecurityDictionary<Guid, TValue> registry;

        public virtual int Count => registry.Count;
        public virtual bool IsEmpty => Count == 0;

        public virtual bool RegValue(TValue value)
        {
            if (HasReg(value.Guid)) return false;
            return registry.AddSecurity(value.Guid, value);
        }

        public virtual TValue GetValue(Guid guid)
        {
            bool result = TryGetValue(guid, out TValue value);
            return result ? value : default;
        }

        public virtual bool TryGetValue(Guid guid, out TValue value)
        {
            bool result = registry.GetSecurity(guid, out TValue val);
            value = val;

            return result;
        }

        public virtual bool Override(TValue value) => registry.OverrideValue(value.Guid, value);

        public virtual bool RemoveValue(Guid guid) => registry.RemoveSecurity(guid);

        public virtual bool HasReg(Guid guid) => registry.Examine(guid);

        /// <summary>
        /// ��������֯��
        /// 
        /// <para>������� <seealso cref="OnTraverse(KeyValuePair{Guid, TExternalValue})"/></para>
        /// </summary>
        public virtual void Traverse()
        {
            List<Guid> guids = new(registry.Keys);
            foreach (Guid guid in guids) OnTraverse(new(guid, GetValue(guid)));
        }

        /// <summary>
        /// ��������ʱ���ô˷�����
        /// 
        /// <para>������� <seealso cref="Traverse"/></para>
        /// </summary>
        protected virtual void OnTraverse(KeyValuePair<Guid, TValue> pair) { }

        protected Organize() : this(new()) { }
        protected Organize(SecurityDictionary<Guid, TValue> dictionary) => registry = dictionary;
    }
}
