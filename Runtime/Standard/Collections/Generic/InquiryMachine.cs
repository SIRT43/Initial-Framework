using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Collections.Generic
{
    public interface IInquiryable<TKey, TValue>
    {
        public TValue Inquiry(TKey key);
        public bool TryInquiry(TKey key, out TValue value);


        public bool Has(TKey key);
    }

    /// <summary>
    /// ��ѯ����
    /// <br>����ֻ���Ҳ���֪���ֵ䣬�����ܲ�ѯ���Ƿ�ӵ��ĳ�</br>
    /// </summary>
    public class InquiryMachine<TKey, TValue> : IInquiryable<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> data;
        protected IReadOnlyDictionary<TKey, TValue> Data => data;

        /// <summary>
        /// �ڴ�֮ǰ���������ṩ���ݡ�
        /// <br>�ڲ�ѯ�����������������޸�����</br>
        /// </summary>
        public InquiryMachine(Dictionary<TKey, TValue> data) => this.data = data;
        /// <summary>
        /// �ڴ�֮ǰ���������ṩ���ݡ�
        /// <br>�ڲ�ѯ�����������������޸�����</br>
        /// </summary>
        public InquiryMachine(IDictionary<TKey, TValue> data) : this(new(data)) { }

        public TValue Inquiry(TKey key)
        {
            if (!Has(key)) return default;
            return data[key];
        }

        public bool TryInquiry(TKey key, out TValue value)
        {
            if (Has(key))
            {
                value = data[key];
                return true;
            }

            value = default;
            return false;
        }

        public bool Has(TKey key) => data.ContainsKey(key);
    }
}
