using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Classifying
{
    /// <summary>
    /// �ɷ��࣬���ڽ�һ��ֵ���ൽ���ڼ����б��С�
    /// </summary>
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public interface IClassifiable<TKey, TValue>
    {
        /// <summary>  
        /// ��һ��ֵ���ൽ���ڼ����б��С�
        /// 
        /// <para>������� <seealso cref="IsCanonical(TValue, out TKey)"/></para>
        /// </summary>  
        public Dictionary<TKey, List<TValue>> Classify(TValue[] values);

        /// <summary>  
        /// ȷ��������ֵ�Ƿ��� "�淶��"����Ϊ������һ������  
        /// </summary>  
        /// <param name="key">���ֵ�ǹ淶�ģ������ɲ�����˼���</param>  
        /// <returns>���ֵ�ǹ淶�ģ���Ϊtrue������Ϊfalse��</returns>
        public bool IsCanonical(TValue value, out TKey key);
    }

    /// <summary>  
    /// ���������࣬ʵ���� <see cref="IClassifiable{TKey, TValue}"/>���ṩ�˷����Ĭ��ʵ�֡�  
    /// </summary>  
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>  
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
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
