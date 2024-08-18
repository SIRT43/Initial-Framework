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
    /// 查询机。
    /// <br>它是只读且不可知的字典，您仅能查询它是否拥有某项。</br>
    /// </summary>
    public class InquiryMachine<TKey, TValue> : IInquiryable<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> data;
        protected IReadOnlyDictionary<TKey, TValue> Data => data;

        /// <summary>
        /// 在此之前，您必须提供数据。
        /// <br>在查询机创建后，您将不能修改它。</br>
        /// </summary>
        public InquiryMachine(Dictionary<TKey, TValue> data) => this.data = data;
        /// <summary>
        /// 在此之前，您必须提供数据。
        /// <br>在查询机创建后，您将不能修改它。</br>
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
