using FTGAMEStudio.InitialFramework.Collections.Generic;
using FTGAMEStudio.InitialFramework.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FTGAMEStudio.InitialFramework.Collections.WeakReference
{
    public interface IWeakReferenceDictionary<TKey, TValue> : IDictionary<TKey, WeakReference<TValue>>, IRefreshable where TValue : class
    {
        public new TValue this[TKey key] { get; set; }

        public new ICollection<TValue> Values { get; }

        public void Add(TKey key, TValue value);

        public bool SetTarget(TKey key, TValue value);

        public bool ContainsValue(TValue value);

        public TValue GetTarget(TKey key);
        public bool TryGetTarget(TKey key, out TValue value);
    }

    /// <summary>
    /// 弱引用字典。
    /// <br>当访问发生，字典会尝试清理无效的键值对。</br>
    /// 
    /// <para>
    /// 另外，弱引用字典的 Count 是不安全的。
    /// <br>如果您需要使用，请先 <see cref="WeakReferenceDictionary{TKey, TValue}.Refresh"/></br>
    /// </para>
    /// </summary>
    public class WeakReferenceDictionary<TKey, TValue> :
        SecurityDictionary<TKey, WeakReference<TValue>>,
        IWeakReferenceDictionary<TKey, TValue>
        where TValue : class
    {
        public new TValue this[TKey key]
        {
            get => GetTarget(key);
            set => OverrideValue(key, new WeakReference<TValue>(value));
        }

        public new ICollection<TValue> Values
        {
            get
            {
                List<TValue> values = new();
                foreach (TKey key in Keys) values.Add(GetTarget(key));

                return values;
            }
        }

        public virtual void Add(TKey key, TValue value) =>
            AddSecurity(key, new WeakReference<TValue>(value));


        public bool SetTarget(TKey key, TValue value)
        {
            if (!GetSecurity(key, out WeakReference<TValue> weakReference)) return false;

            weakReference.SetTarget(value);
            return true;
        }

        public bool ContainsValue(TValue value)
        {
            foreach (TKey key in Keys)
            {
                if (!TryGetTarget(key, out TValue target)) continue;
                if (value == target) return true;
            }
            return false;
        }

        /// <summary>
        /// 直接获取弱引用目标。
        /// 
        /// <para>
        /// 本方法会先执行 Examine 方法，当 Examine 返回 false 时本方法会直接返回 null。
        /// <br>另请参阅 <see cref="Examine(TKey)"/></br>
        /// </para>
        /// </summary>
        public TValue GetTarget(TKey key)
        {
            if (!Examine(key)) return null;

            return base[key].GetTarget();
        }

        /// <summary>
        /// 尝试获取弱引用目标，返回 true 时则获取成功，返回 false 时则获取失败。
        /// 
        /// <para>
        /// 本方法会先执行 Examine 方法，当 Examine 返回 false 时本方法会直接返回 null。
        /// <br>另请参阅 <see cref="Examine(TKey)"/></br>
        /// </para>
        /// </summary>
        public bool TryGetTarget(TKey key, out TValue target)
        {
            if (Examine(key))
            {
                target = base[key].GetTarget();
                return true;
            }

            target = null;
            return false;
        }

        /// <summary>
        /// 检查指定的键值对。
        /// 
        /// <para>对于以下几种情况，本方法的解决方案是：
        /// <br>访问的键不存在：返回 false。</br>
        /// <br>访问的键存在但值为 null：移除键值对并返回 false。</br>
        /// <br>访问的键存在但弱引用目标值为 null：移除键值对并返回 false。</br>
        /// <br>访问的键存在且值与弱引用目标都存在：返回 true。</br></para>
        /// </summary>
        public override bool Examine(TKey key)
        {
            if (!base.Examine(key)) return false;

            TValue result = base[key].GetTarget();

            if (result == null)
            {
                Remove(key);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 刷新键值，移除无效项目。
        /// 
        /// <para>本方法基于 <see cref="Examine(TKey)"/> 方法。</para>
        /// </summary>
        public void Refresh()
        {
            List<TKey> keys = new(Keys);
            foreach (TKey key in keys) Examine(key);
        }

        public override bool IsValid(WeakReference<TValue> value)
        {
            if (!base.IsValid(value)) return false;
            return value.TryGetTarget(out _);
        }

        public WeakReferenceDictionary() : base() { }
        public WeakReferenceDictionary(IDictionary<TKey, WeakReference<TValue>> dictionary) : base(dictionary) { }
        public WeakReferenceDictionary(IDictionary<TKey, WeakReference<TValue>> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }
        public WeakReferenceDictionary(IEnumerable<KeyValuePair<TKey, WeakReference<TValue>>> collection) : base(collection) { }
        public WeakReferenceDictionary(IEnumerable<KeyValuePair<TKey, WeakReference<TValue>>> collection, IEqualityComparer<TKey> comparer) : base(collection, comparer) { }
        public WeakReferenceDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }
        public WeakReferenceDictionary(int capacity) : base(capacity) { }
        public WeakReferenceDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
        protected WeakReferenceDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
