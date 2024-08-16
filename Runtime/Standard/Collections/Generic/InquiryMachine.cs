using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Collections.Generic
{
    public interface IInquiryable<TKey, TValue>
    {
        public TValue Inquiry(TKey key);
        public bool TryInquiry(TKey key, out TValue value);


        public bool Has(TKey key);
    }

    public class InquiryMachine<TKey, TValue> : IInquiryable<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> pairs = new();
        protected IReadOnlyDictionary<TKey, TValue> Pairs => pairs;

        public InquiryMachine(Dictionary<TKey, TValue> pairs) => this.pairs = pairs;

        public TValue Inquiry(TKey key)
        {
            if (!Has(key)) return default;
            return pairs[key];
        }

        public bool TryInquiry(TKey key, out TValue value)
        {
            if (Has(key))
            {
                value = pairs[key];
                return true;
            }

            value = default;
            return false;
        }

        public bool Has(TKey key) => pairs.ContainsKey(key);
    }
}
