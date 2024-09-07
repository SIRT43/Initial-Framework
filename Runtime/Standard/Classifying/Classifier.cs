using FTGAMEStudio.InitialFramework.Traverse;
using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Classifying
{
    /// <summary>
    /// �ɷ��࣬���ڽ�һ��ֵ���ൽ���ڼ����б��С�
    /// </summary>
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public interface IClassifiable<TKey, TValue> : IFlowTraversable<TValue[], TValue>
    {
        /// <summary>
        /// ��һ��ֵ���ൽ���ڼ����б��С�
        /// </summary>
        Dictionary<TKey, List<TValue>> Classify(TValue[] values);

        /// <summary>  
        /// Ϊָ��ֵ����һ������  
        /// </summary>  
        TKey GenerateKey(TValue value);
    }

    /// <summary>  
    /// ���������࣬ʵ���� <see cref="IClassifiable{TKey, TValue}"/>���ṩ�˷����Ĭ��ʵ�֡�  
    /// </summary>  
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>  
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public abstract class Classifier<TKey, TValue> : Traverser<TValue[], TValue>, IClassifiable<TKey, TValue>
    {
        private Dictionary<TKey, List<TValue>> classify;


        protected Classifier() : base(FlowControl.Continue) { }


        public virtual Dictionary<TKey, List<TValue>> Classify(TValue[] values)
        {
            Dictionary<TKey, List<TValue>>  classify = new();
            this.classify = classify;

            Traverse(values);

            this.classify = null;

            return classify;
        }

        protected override void OnTraverse(TValue value)
        {
            TKey key = GenerateKey(value);

            if (!classify.ContainsKey(key)) classify.Add(key, new() { value });
            else classify[key].Add(value);
        }

        public abstract TKey GenerateKey(TValue value);
    }
}
