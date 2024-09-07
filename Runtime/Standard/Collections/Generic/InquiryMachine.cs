using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Collections.Generic
{
    public interface IInquiryable<TKey, TValue>
    {
        TValue Inquiry(TKey key);
        bool TryInquiry(TKey key, out TValue value);

        bool Has(TKey key);
    }

    /// <summary>
    /// ��ѯ����
    /// <br>����ֻ���Ҳ���֪���ֵ����ڴ���ʱ��ȷ���˼�ֵ�������ܲ�ѯ���Ƿ�ӵ��ĳ�</br>
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
        /// <summary>
        /// �ڴ�֮ǰ���������ṩ���ݡ�
        /// <br>�ڲ�ѯ�����������������޸�����</br>
        /// </summary>
        public InquiryMachine(IEnumerable<KeyValuePair<TKey, TValue>> data) : this(new(data)) { }

        public virtual TValue Inquiry(TKey key)
        {
            TryInquiry(key, out TValue value);
            return value;
        }

        public virtual bool TryInquiry(TKey key, out TValue value)
        {
            if (Has(key))
            {
                value = data[key];
                return true;
            }

            value = default;
            return false;
        }

        public virtual bool Has(TKey key) => data.ContainsKey(key);
    }
}
