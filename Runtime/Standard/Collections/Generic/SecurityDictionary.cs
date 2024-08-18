using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FTGAMEStudio.InitialFramework.Collections.Generic
{
    public interface ISecurityDictionary<TKey, TValue>
    {
        public bool AddSecurity(TKey key, TValue value);
        public bool SetSecurity(TKey key, TValue value);

        public bool GetSecurity(TKey key, out TValue value);

        public bool OverrideValue(TKey key, TValue value);

        public bool RemoveSecurity(TKey key);

        /// <summary>
        /// 检查键值对是否有效，所有安全操作都将基于本方法。
        /// </summary>
        public bool IsValid(TKey key, TValue value);
        public bool IsValid(TKey key);
        public bool IsValid(TValue value);

        public bool Examine(TKey key);
    }

    /// <summary>  
    /// <see cref="SecurityDictionary{TKey, TValue}"/> 类是一个字典，它提供了一些扩展功能，
    /// <br>如安全地添加或更新键值对、处理 null 值或不存在的键值对等，以满足特定的安全或业务逻辑需求。</br>
    /// 
    /// <para>
    /// 另请参阅 <see cref="Dictionary{TKey, TValue}"/>，<see cref="ISecurityDictionary{TKey, TValue}"/>。
    /// </para>
    /// </summary>  
    public class SecurityDictionary<TKey, TValue> :
        Dictionary<TKey, TValue>,
        ISecurityDictionary<TKey, TValue>
    {
        public virtual bool AddSecurity(TKey key, TValue value)
        {
            if (!IsValid(key, value)) return false;
            return TryAdd(key, value);
        }

        /// <summary>
        /// 安全的设置值，当键不存在时则自动创建键值对。
        /// </summary>
        public virtual bool SetSecurity(TKey key, TValue value)
        {
            if (!IsValid(value)) return false;

            if (!Examine(key)) Add(key, value);
            else base[key] = value;

            return true;
        }

        /// <summary>
        /// 本方法与 <see cref="Dictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/> 不同，
        /// <br>当键存在但关联的值为 null 时，此方法将返回 false，标准 <see cref="Dictionary{TKey, TValue}.TryGetValue"/> 方法后者在值为 null 时仍返回 true。</br>
        /// </summary>
        public virtual bool GetSecurity(TKey key, out TValue value)
        {
            if (Examine(key))
            {
                value = base[key];
                return true;
            }

            value = default;
            return false;
        }

        public virtual bool OverrideValue(TKey key, TValue value)
        {
            if (!IsValid(value)) return false;

            if (!Examine(key)) return false;

            base[key] = value;
            return true;
        }

        public virtual bool RemoveSecurity(TKey key)
        {
            if (!IsValid(key)) return false;
            return Remove(key);
        }

        public virtual bool IsValid(TKey key, TValue value) => IsValid(key) && IsValid(value);
        public virtual bool IsValid(TKey key) => key != null;
        public virtual bool IsValid(TValue value) => value != null;

        /// <summary>
        /// 检查指定的键值对，本方法将基于 <see cref="IsValid"/>。
        /// <br>如果访问的键存在但对应的值为 null，则此方法会自动从字典中移除该键值对，并返回 false，以确保字典中不包含任何 null 值。</br>
        /// 
        /// <para>
        /// 对于以下几种情况，本方法的解决方案是：
        /// <br>访问的键不存在：返回 false。</br>
        /// <br>访问的键存在但值为 null：移除键值对并返回 false。</br>
        /// <br>访问的键值对存在：返回 true。</br>
        /// </para>
        /// </summary>
        public virtual bool Examine(TKey key)
        {
            if (!IsValid(key)) return false;

            if (!ContainsKey(key)) return false;

            if (!IsValid(base[key]))
            {
                Remove(key);
                return false;
            }

            return true;
        }

        public SecurityDictionary() : base() { }
        public SecurityDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
        public SecurityDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }
        public SecurityDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection) { }
        public SecurityDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : base(collection, comparer) { }
        public SecurityDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }
        public SecurityDictionary(int capacity) : base(capacity) { }
        public SecurityDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
        protected SecurityDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
