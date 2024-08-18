using System;
using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Collections.Generic
{
    /// <summary>
    /// 当您创建受管理的对象时，请继承本接口。
    /// <br>另请参阅 <seealso cref="OrganizeBase{TDictionary, TValue}"/></br>
    /// </summary>
    public interface IOrganized
    {
        /// <summary>
        /// 本对象的 <see cref="System.Guid"/>。
        /// <br>如果没有其他要求，您应该将此字段初始化为编译时常量。</br>
        /// </summary>
        public Guid Guid { get; }
    }

    public interface IOrganizer<T> where T : IOrganized
    {
        /// <summary>
        /// 注册值，值的 <see cref="Guid"/> 应该是唯一的。
        /// </summary>
        public bool RegValue(T value);

        public T GetValue(Guid guid);
        public bool TryGetValue(Guid guid, out T value);

        public bool HasReg(Guid guid);

        public bool Override(T value);

        public bool RemoveValue(Guid guid);
    }

    /// <summary>
    /// 本类是一个抽象基类，用于管理实现了 <see cref="IOrganized"/> 接口的对象。
    /// 
    /// <para>另请参阅 <seealso cref="IOrganized"/>，<seealso cref="IOrganizer{T}"/></para>
    /// </summary>
    public abstract class Organize<TValue> : IOrganizer<TValue> where TValue : IOrganized
    {
        /// <summary>
        /// 注册表。
        /// <br>如果您没有特殊要求，请向本字典注册对象。</br>
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
        /// 遍历此组织。
        /// 
        /// <para>另请参阅 <seealso cref="OnTraverse(KeyValuePair{Guid, TExternalValue})"/></para>
        /// </summary>
        public virtual void Traverse()
        {
            List<Guid> guids = new(registry.Keys);
            foreach (Guid guid in guids) OnTraverse(new(guid, GetValue(guid)));
        }

        /// <summary>
        /// 当被遍历时调用此方法。
        /// 
        /// <para>另请参阅 <seealso cref="Traverse"/></para>
        /// </summary>
        protected virtual void OnTraverse(KeyValuePair<Guid, TValue> pair) { }

        protected Organize() : this(new()) { }
        protected Organize(SecurityDictionary<Guid, TValue> dictionary) => registry = dictionary;
    }
}
