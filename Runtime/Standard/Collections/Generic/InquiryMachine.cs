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
    /// 查询机。
    /// <br>它是只读且不可知的字典且在创建时就确定了键值，您仅能查询它是否拥有某项。</br>
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
        /// <summary>
        /// 在此之前，您必须提供数据。
        /// <br>在查询机创建后，您将不能修改它。</br>
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
