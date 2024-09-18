using System;
using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    /// <summary>    
    /// ���ɹ��˷���������ʹ���ṩ��ί�������˺ͷ���ֵ��
    /// 
    /// <para>
    /// <br>�����Ϊ��Щ��Ҫ��������߼��ĳ����ṩ�˱�����</br>
    /// <br>ʹ�� <see cref="FilterableMonoClassifier{TKey, TValue}"/> �������ڲ��޸��ඨ�������£�ͨ�����ݲ�ͬ�Ĺ��˹������ı������Ϊ��</br>
    /// </para>
    /// </summary>    
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>    
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public class FilterableMonoClassifier<TKey, TValue> : MonoClassifier<TKey, TValue>, IMonoFilterable<TKey, TValue>
    {
        public virtual event Func<TValue, Dictionary<TKey, TValue>, TKey> KeyGenerator;
        public virtual event Func<TValue, Dictionary<TKey, TValue>, bool> ValueFilter;

        /// <summary>
        /// �ڴ�֮ǰ�����������ƹ�������
        /// </summary>
        public FilterableMonoClassifier(Func<TValue, Dictionary<TKey, TValue>, TKey> keyGenerator, Func<TValue, Dictionary<TKey, TValue>, bool> valueFilter = null)
        {
            KeyGenerator = keyGenerator;
            ValueFilter = valueFilter;
        }

        public override bool IsCanonical(TValue value, Dictionary<TKey, TValue> context) => ValueFilter == null || ValueFilter.Invoke(value, context);
        public override TKey GenerateKey(TValue value, Dictionary<TKey, TValue> context) => KeyGenerator.Invoke(value, context);
    }

    /// <summary>    
    /// ��ɹ��˷���������ʹ���ṩ��ί�������˺ͷ���ֵ��
    /// 
    /// <para>
    /// <br>�����Ϊ��Щ��Ҫ��������߼��ĳ����ṩ�˱�����</br>
    /// <br>ʹ�� <see cref="FilterableMultiClassifier{TKey, TValue}"/> �������ڲ��޸��ඨ�������£�ͨ�����ݲ�ͬ�Ĺ��˹������ı������Ϊ��</br>
    /// </para>
    /// </summary>    
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>    
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public class FilterableMultiClassifier<TKey, TValue> : MultiClassifier<TKey, TValue>, IMultiFilterable<TKey, TValue>
    {
        public virtual event Func<TValue, Dictionary<TKey, List<TValue>>, TKey> KeyGenerator;
        public virtual event Func<TValue, Dictionary<TKey, List<TValue>>, bool> ValueFilter;

        /// <summary>
        /// �ڴ�֮ǰ�����������ƹ�������
        /// </summary>
        public FilterableMultiClassifier(Func<TValue, Dictionary<TKey, List<TValue>>, TKey> keyGenerator, Func<TValue, Dictionary<TKey, List<TValue>>, bool> valueFilter = null)
        {
            KeyGenerator = keyGenerator;
            ValueFilter = valueFilter;
        }

        public override bool IsCanonical(TValue value, Dictionary<TKey, List<TValue>> context) => ValueFilter == null || ValueFilter.Invoke(value, context);
        public override TKey GenerateKey(TValue value, Dictionary<TKey, List<TValue>> context) => KeyGenerator.Invoke(value, context);
    }
}
