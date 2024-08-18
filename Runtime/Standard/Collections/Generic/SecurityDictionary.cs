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
        /// ����ֵ���Ƿ���Ч�����а�ȫ�����������ڱ�������
        /// </summary>
        public bool IsValid(TKey key, TValue value);
        public bool IsValid(TKey key);
        public bool IsValid(TValue value);

        public bool Examine(TKey key);
    }

    /// <summary>  
    /// <see cref="SecurityDictionary{TKey, TValue}"/> ����һ���ֵ䣬���ṩ��һЩ��չ���ܣ�
    /// <br>�簲ȫ����ӻ���¼�ֵ�ԡ����� null ֵ�򲻴��ڵļ�ֵ�Եȣ��������ض��İ�ȫ��ҵ���߼�����</br>
    /// 
    /// <para>
    /// ������� <see cref="Dictionary{TKey, TValue}"/>��<see cref="ISecurityDictionary{TKey, TValue}"/>��
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
        /// ��ȫ������ֵ������������ʱ���Զ�������ֵ�ԡ�
        /// </summary>
        public virtual bool SetSecurity(TKey key, TValue value)
        {
            if (!IsValid(value)) return false;

            if (!Examine(key)) Add(key, value);
            else base[key] = value;

            return true;
        }

        /// <summary>
        /// �������� <see cref="Dictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/> ��ͬ��
        /// <br>�������ڵ�������ֵΪ null ʱ���˷��������� false����׼ <see cref="Dictionary{TKey, TValue}.TryGetValue"/> ����������ֵΪ null ʱ�Է��� true��</br>
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
        /// ���ָ���ļ�ֵ�ԣ������������� <see cref="IsValid"/>��
        /// <br>������ʵļ����ڵ���Ӧ��ֵΪ null����˷������Զ����ֵ����Ƴ��ü�ֵ�ԣ������� false����ȷ���ֵ��в������κ� null ֵ��</br>
        /// 
        /// <para>
        /// �������¼���������������Ľ�������ǣ�
        /// <br>���ʵļ������ڣ����� false��</br>
        /// <br>���ʵļ����ڵ�ֵΪ null���Ƴ���ֵ�Բ����� false��</br>
        /// <br>���ʵļ�ֵ�Դ��ڣ����� true��</br>
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
