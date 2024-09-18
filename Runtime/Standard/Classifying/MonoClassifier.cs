using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    /// <summary>
    /// ���ɷ��࣬���ڽ�һ��ֵ���ൽ���ڼ����б��С�
    /// </summary>
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public interface IMonoClassifiable<TKey, TValue> : IClassifiable<TKey, TValue, Dictionary<TKey, TValue>> { }

    /// <summary>  
    /// �����������࣬ʵ���� <see cref="IMonoClassifiable{TKey, TValue}"/>���ṩ�˷����Ĭ��ʵ�֡�  
    /// </summary>  
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>  
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public abstract class MonoClassifier<TKey, TValue> : ClassifierBase<TKey, TValue, Dictionary<TKey, TValue>>, IMonoClassifiable<TKey, TValue>
    {
        protected override void OnTraverse(TValue value, Dictionary<TKey, TValue> context) => context.Add(GenerateKey(value, context), value);
    }
}
