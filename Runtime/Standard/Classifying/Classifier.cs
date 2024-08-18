using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Classifying
{
    /// <summary>
    /// 可分类，用于将一组值分类到基于键的列表中。
    /// </summary>
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public interface IClassifiable<TKey, TValue>
    {
        /// <summary>  
        /// 将一组值分类到基于键的列表中。
        /// 
        /// <para>另请参阅 <seealso cref="IsCanonical(TValue, out TKey)"/></para>
        /// </summary>  
        public Dictionary<TKey, List<TValue>> Classify(TValue[] values);

        /// <summary>  
        /// 确定给定的值是否是 "规范的"，并为其生成一个键。  
        /// </summary>  
        /// <param name="key">如果值是规范的，则生成并输出此键。</param>  
        /// <returns>如果值是规范的，则为true；否则为false。</returns>
        public bool IsCanonical(TValue value, out TKey key);
    }

    /// <summary>  
    /// 分类器基类，实现了 <see cref="IClassifiable{TKey, TValue}"/>，提供了分类的默认实现。  
    /// </summary>  
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>  
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public abstract class Classifier<TKey, TValue> : IClassifiable<TKey, TValue>
    {
        public virtual Dictionary<TKey, List<TValue>> Classify(TValue[] values)
        {
            Dictionary<TKey, List<TValue>> classify = new();

            foreach (TValue value in values)
            {
                if (!IsCanonical(value, out TKey key)) continue;

                if (!classify.ContainsKey(key)) classify.Add(key, new() { value });
                else classify[key].Add(value);
            }

            return classify;
        }

        public abstract bool IsCanonical(TValue value, out TKey key);
    }
}
