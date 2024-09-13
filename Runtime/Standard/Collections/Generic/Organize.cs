using InitialFramework.Traverse;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InitialFramework.Collections.Generic
{
    /// <summary>
    /// ���������ܹ���Ķ���ʱ����̳б��ӿڡ�
    /// <br>������� <seealso cref="Organize{TValue, TTraverser}"/></br>
    /// </summary>
    public interface IOrganized
    {
        /// <summary>
        /// ������� <see cref="System.Guid"/>��
        /// <br>���û������Ҫ����Ӧ�ý����ֶγ�ʼ��Ϊ����ʱ������</br>
        /// </summary>
        Guid Guid { get; }
    }

    public interface IOrganizer<T> where T : IOrganized
    {
        /// <summary>
        /// ע��ֵ��ֵ�� <see cref="Guid"/> Ӧ����Ψһ�ġ�
        /// </summary>
        bool RegValue(T value);

        T GetValue(Guid guid);
        bool TryGetValue(Guid guid, out T value);

        bool HasReg(Guid guid);

        bool RemoveValue(Guid guid);
    }

    /// <summary>
    /// �������ڹ���ʵ���� <see cref="IOrganized"/> �ӿڵĶ���
    /// 
    /// <para>������� <seealso cref="IOrganized"/>��<seealso cref="IOrganizer{T}"/></para>
    /// </summary>
    [Serializable]
    public class Organize<TValue, TTraverser> :
        IOrganizer<TValue>,
        ITraversable<TTraverser, Dictionary<Guid, TValue>.ValueCollection, TValue>
        where TValue : IOrganized
        where TTraverser : Traverser<Dictionary<Guid, TValue>.ValueCollection, TValue>, new()
    {
        /// <summary>
        /// ע���
        /// <br>�����û������Ҫ�������ֵ�ע�����</br>
        /// </summary>
        [SerializeField] protected ValidDictionary<Guid, TValue> registry;

        public virtual int Count => registry.Count;
        public virtual bool IsEmpty => Count == 0;

        public TTraverser Traverser { get; set; }


        public virtual bool RegValue(TValue value)
        {
            if (HasReg(value.Guid)) return false;

            registry.Add(value.Guid, value);
            return true;
        }

        public virtual TValue GetValue(Guid guid)
        {
            bool result = TryGetValue(guid, out TValue value);
            return result ? value : default;
        }

        public virtual bool TryGetValue(Guid guid, out TValue value)
        {
            bool result = registry.TryGetValue(guid, out TValue val);
            value = val;

            return result;
        }

        public virtual bool RemoveValue(Guid guid) => registry.Remove(guid);

        public virtual bool HasReg(Guid guid) => registry.Examine(guid);

        /// <summary>
        /// ��������֯��
        /// 
        /// <para>������� <seealso cref="Traverser{TValue, TEnumerable}"/>��</para>
        /// </summary>
        public virtual void Traverse() => Traverser.Traverse(registry.Values);



        public Organize(ValidDictionary<Guid, TValue> dictionary, TTraverser traverser)
        {
            registry = dictionary;
            Traverser = traverser;
        }
        public Organize(ValidDictionary<Guid, TValue> dictionary) : this(dictionary, new()) { }
        public Organize(TTraverser traverser) : this(new(), traverser) { }
        public Organize() : this(new(), new()) { }
    }
}
