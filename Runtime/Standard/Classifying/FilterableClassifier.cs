using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    /// <summary>    
    /// �ɹ��˷���������ʹ���ṩ��ί�������˺ͷ���ֵ��
    /// 
    /// <para>
    /// <br>�����Ϊ��Щ��Ҫ��������߼��ĳ����ṩ�˱�����</br>
    /// <br>ʹ�� <see cref="FilterableClassifier{TKey, TValue}"/> �������ڲ��޸��ඨ�������£�ͨ�����ݲ�ͬ�Ĺ��˹������ı������Ϊ��</br>
    /// </para>
    /// </summary>    
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>    
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public class FilterableClassifier<TKey, TValue> : Classifier<TKey, TValue>, IFilterable<TKey, TValue>
    {
        public event ValueFilter<TKey, TValue> ValueFilter;
        public event KeyGenerator<TKey, TValue> KeyGenerator;

        /// <summary>
        /// �ڴ�֮ǰ�����������ƹ�������
        /// </summary>
        public FilterableClassifier(ValueFilter<TKey, TValue> valueFilter, KeyGenerator<TKey, TValue> keyGenerator)
        {
            ValueFilter = valueFilter;
            KeyGenerator = keyGenerator;
        }

        public override bool IsCanonical(TValue value, Dictionary<TKey, List<TValue>> context) => ValueFilter != null && ValueFilter.Invoke(value, context);
        public override TKey GenerateKey(TValue value, Dictionary<TKey, List<TValue>> context) => KeyGenerator.Invoke(value, context);
    }
}
