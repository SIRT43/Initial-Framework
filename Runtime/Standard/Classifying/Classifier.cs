using InitialFramework.Traverse;
using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    /// <summary>
    /// �ɷ��࣬���ڽ�һ��ֵ���ൽ���ڼ����б��С�
    /// </summary>
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public interface IClassifiable<TKey, TValue> : IFlowTraversable<TValue, Dictionary<TKey, List<TValue>>>
    {
        /// <summary>
        /// ��һ��ֵ���ൽ���ڼ����б��С�
        /// </summary>
        Dictionary<TKey, List<TValue>> Classify(IEnumerable<TValue> values);

        /// <summary>  
        /// Ϊָ��ֵ����һ������  
        /// </summary>  
        TKey GenerateKey(TValue value, Dictionary<TKey, List<TValue>> context);
    }

    /// <summary>  
    /// ���������࣬ʵ���� <see cref="IClassifiable{TKey, TValue}"/>���ṩ�˷����Ĭ��ʵ�֡�  
    /// </summary>  
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>  
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public abstract class Classifier<TKey, TValue> : Traverser<TValue, Dictionary<TKey, List<TValue>>>, IClassifiable<TKey, TValue>
    {
        protected Classifier() : base(FlowControl.Continue) { }


        public virtual Dictionary<TKey, List<TValue>> Classify(IEnumerable<TValue> values)
        {
            Dictionary<TKey, List<TValue>> classify = new();

            Traverse(values, classify);

            return classify;
        }

        protected override void OnTraverse(TValue value, Dictionary<TKey, List<TValue>> context)
        {
            TKey key = GenerateKey(value, context);

            if (!context.ContainsKey(key)) context.Add(key, new() { value });
            else context[key].Add(value);
        }

        public abstract TKey GenerateKey(TValue value, Dictionary<TKey, List<TValue>> context);
    }
}
