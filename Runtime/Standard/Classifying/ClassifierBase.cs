using InitialFramework.Traverse;
using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    /// <summary>
    /// 可分类，用于将一组值分类到基于键的列表中。
    /// </summary>
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public interface IClassifiable<TKey, TValue, TContext> : IFlowTraverser<TValue, TContext> where TContext : new()
    {
        /// <summary>
        /// 将一组值分类到基于键的列表中。
        /// </summary>
        TContext Classify(IEnumerable<TValue> values);

        /// <summary>  
        /// 为指定值生成一个键。  
        /// </summary>  
        TKey GenerateKey(TValue value, TContext context);
    }

    /// <summary>  
    /// 分类器基类，实现了 <see cref="IClassifiable{TKey, TValue, TContext}"/>，提供了分类的默认实现。  
    /// </summary>  
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>  
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public abstract class ClassifierBase<TKey, TValue, TContext> : Traverser<TValue, TContext>, IClassifiable<TKey, TValue, TContext> where TContext : new()
    {
        protected ClassifierBase() : base(FlowControl.Continue) { }

        /// <summary>
        /// 将一组值分类到基于键的列表中。
        /// <br>本方法实例化了一个 <see cref="TContext"/> (classify) 随后调用 Traverse 方法 参数为 values, classify</br>
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
