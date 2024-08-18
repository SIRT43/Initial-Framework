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
    /// �������ֵ䡣
    /// <br>�����ʷ������ֵ�᳢��������Ч�ļ�ֵ�ԡ�</br>
    /// 
    /// <para>
    /// ���⣬�������ֵ�� Count �ǲ���ȫ�ġ�
    /// <br>�������Ҫʹ�ã����� <see cref="WeakReferenceDictionary{TKey, TValue}.Refresh"/></br>
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
        /// ֱ�ӻ�ȡ������Ŀ�ꡣ
        /// 
        /// <para>
        /// ����������ִ�� Examine �������� Examine ���� false ʱ��������ֱ�ӷ��� null��
        /// <br>������� <see cref="Examine(TKey)"/></br>
        /// </para>
        /// </summary>
        public TValue GetTarget(TKey key)
        {
            if (!Examine(key)) return null;

            return base[key].GetTarget();
        }

        /// <summary>
        /// ���Ի�ȡ������Ŀ�꣬���� true ʱ���ȡ�ɹ������� false ʱ���ȡʧ�ܡ�
        /// 
        /// <para>
        /// ����������ִ�� Examine �������� Examine ���� false ʱ��������ֱ�ӷ��� null��
        /// <br>������� <see cref="Examine(TKey)"/></br>
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
        /// ���ָ���ļ�ֵ�ԡ�
        /// 
        /// <para>�������¼���������������Ľ�������ǣ�
        /// <br>���ʵļ������ڣ����� false��</br>
        /// <br>���ʵļ����ڵ�ֵΪ null���Ƴ���ֵ�Բ����� false��</br>
        /// <br>���ʵļ����ڵ�������Ŀ��ֵΪ null���Ƴ���ֵ�Բ����� false��</br>
        /// <br>���ʵļ�������ֵ��������Ŀ�궼���ڣ����� true��</br></para>
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
        /// ˢ�¼�ֵ���Ƴ���Ч��Ŀ��
        /// 
        /// <para>���������� <see cref="Examine(TKey)"/> ������</para>
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
