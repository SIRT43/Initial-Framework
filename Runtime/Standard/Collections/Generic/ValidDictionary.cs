using System.Collections.Generic;
using System.Runtime.Serialization;

namespace InitialFramework.Collections.Generic
{
    public interface IValidDictionary<TKey, TValue>
    {
        /// <summary>
        /// ����ֵ���Ƿ���Ч����ȫ�������Ի��ڱ�������
        /// </summary>
        bool IsValid(TKey key, TValue value);
        bool IsValid(TKey key);
        bool IsValid(TValue value);

        bool Examine(TKey key);
    }

    /// <summary>  
    /// <see cref="ValidDictionary{TKey, TValue}"/> ����һ���ֵ䣬���ṩ���ж�ֵ�Ƿ���Ч�Ĺ��ܡ� 
    /// 
    /// <para>
    /// ������� <see cref="Dictionary{TKey, TValue}"/>��<see cref="IValidDictionary{TKey, TValue}"/>��
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
