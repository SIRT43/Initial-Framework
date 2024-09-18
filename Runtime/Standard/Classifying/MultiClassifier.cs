using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    /// <summary>
    /// ��ɷ��࣬���ڽ�һ��ֵ���ൽ���ڼ����б��С�
    /// </summary>
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public interface IMultiClassifiable<TKey, TValue> : IClassifiable<TKey, TValue, Dictionary<TKey, List<TValue>>> { }

    /// <summary>  
    /// ����������࣬ʵ���� <see cref="IMultiClassifiable{TKey, TValue}"/>���ṩ�˷����Ĭ��ʵ�֡�  
    /// </summary>  
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>  
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public abstract class MultiClassifier<TKey, TValue> : ClassifierBase<TKey, TValue, Dictionary<TKey, List<TValue>>>, IMultiClassifiable<TKey, TValue>
    {
        protected override void OnTraverse(TValue value, Dictionary<TKey, List<TValue>> context)
        {
            TKey key = GenerateKey(value, context);

            if (!context.ContainsKey(key)) context.Add(key, new() { value });
            else context[key].Add(value);
        }
    }
}
