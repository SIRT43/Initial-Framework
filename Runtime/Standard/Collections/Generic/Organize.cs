using InitialFramework.Traverse;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InitialFramework.Collections.Generic
{
    /// <summary>
    /// 当您创建受管理的对象时，请继承本接口。
    /// <br>另请参阅 <seealso cref="Organize{TValue}"/></br>
    /// </summary>
    public interface IOrganized
    {
        /// <summary>
        /// 本对象的 <see cref="System.Guid"/>。
        /// <br>如果没有其他要求，您应该将此字段初始化为编译时常量。</br>
        /// </summary>
        Guid Guid { get; }
    }

    public interface IOrganizer<T> where T : IOrganized
    {
        /// <summary>
        /// 注册值，值的 <see cref="Guid"/> 应该是唯一的。
        /// </summary>
        bool RegValue(T value);

        T GetValue(Guid guid);
        bool TryGetValue(Guid guid, out T value);

        bool HasReg(Guid guid);

        bool RemoveValue(Guid guid);
    }

    /// <summary>
    /// 本类用于管理实现了 <see cref="IOrganized"/> 接口的对象。
    /// 
    /// <para>另请参阅 <seealso cref="IOrganized"/>，<seealso cref="IOrganizer{T}"/></para>
    /// </summary>
    [Serializable]
    public class Organize<TValue> : IOrganizer<TValue>, IEnumerable<KeyValuePair<Guid, TValue>>, ITraversable where TValue : IOrganized
    {
        /// <summary>
        /// 注册表。
        /// <br>如果您没有特殊要求，请向本字典注册对象。</br>
        /// </summary>
        [SerializeField] protected ValidDictionary<Guid, TValue> registry;

        public virtual int Count => registry.Count;
        public virtual bool IsEmpty => Count == 0;


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


        public virtual void Traverse()
        {
            List<Guid> guids = new(registry.Keys);
            foreach (Guid guid in guids) OnTraverse(registry[guid]);
        }

        protected virtual void OnTraverse(TValue value) { }


        public IEnumerator<KeyValuePair<Guid, TValue>> GetEnumerator() => registry.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => registry.GetEnumerator();


        public Organize(ValidDictionary<Guid, TValue> dictionary) => registry = dictionary;
        public Organize() : this(new()) { }
    }
}
