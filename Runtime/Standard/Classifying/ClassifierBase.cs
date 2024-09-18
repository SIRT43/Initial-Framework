using InitialFramework.Traverse;
using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    /// <summary>
    /// �ɷ��࣬���ڽ�һ��ֵ���ൽ���ڼ����б��С�
    /// </summary>
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public interface IClassifiable<TKey, TValue, TContext> : IFlowTraverser<TValue, TContext> where TContext : new()
    {
        /// <summary>
        /// ��һ��ֵ���ൽ���ڼ����б��С�
        /// </summary>
        TContext Classify(IEnumerable<TValue> values);

        /// <summary>  
        /// Ϊָ��ֵ����һ������  
        /// </summary>  
        TKey GenerateKey(TValue value, TContext context);
    }

    /// <summary>  
    /// ���������࣬ʵ���� <see cref="IClassifiable{TKey, TValue, TContext}"/>���ṩ�˷����Ĭ��ʵ�֡�  
    /// </summary>  
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>  
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public abstract class ClassifierBase<TKey, TValue, TContext> : Traverser<TValue, TContext>, IClassifiable<TKey, TValue, TContext> where TContext : new()
    {
        protected ClassifierBase() : base(FlowControl.Continue) { }

        /// <summary>
        /// ��һ��ֵ���ൽ���ڼ����б��С�
        /// <br>������ʵ������һ�� <see cref="TContext"/> (classify) ������ Traverse ���� ����Ϊ values, classify</br>
        /// </summary>
        public virtual TContext Classify(IEnumerable<TValue> values)
        {
            TContext classify = new();
            Traverse(values, classify);

            return classify;
        }

        public abstract TKey GenerateKey(TValue value, TContext context);
    }
}
