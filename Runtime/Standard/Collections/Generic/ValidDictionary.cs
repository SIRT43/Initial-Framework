using System.Collections.Generic;
using System.Runtime.Serialization;

namespace InitialFramework.Collections.Generic
{
    public interface IValidDictionary<TKey, TValue>
    {
        /// <summary>
        /// 检查键值对是否有效，安全操作可以基于本方法。
        /// </summary>
        bool IsValid(TKey key, TValue value);
        bool IsValid(TKey key);
        bool IsValid(TValue value);

        bool Examine(TKey key);
    }

    /// <summary>  
    /// <see cref="ValidDictionary{TKey, TValue}"/> 类是一个字典，它提供了判断值是否有效的功能。 
    /// 
    /// <para>
    /// 另请参阅 <see cref="Dictionary{TKey, TValue}"/>，<see cref="IValidDictionary{TKey, TValue}"/>。
    /// </para>
    /// </summary>  
    public class ValidDictionary<TKey, TValue> :
        Dictionary<TKey, TValue>,
        IValidDictionary<TKey, TValue>
    {
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



        public ValidDictionary() : base() { }
        public ValidDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
        public ValidDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }
        public ValidDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection) { }
        public ValidDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : base(collection, comparer) { }
        public ValidDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }
        public ValidDictionary(int capacity) : base(capacity) { }
        public ValidDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
        protected ValidDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
