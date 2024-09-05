namespace FTGAMEStudio.InitialFramework.Classifying
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
        public event ValueFilter<TValue> ValueFilter;
        public event KeyGenerator<TKey, TValue> KeyGenerator;

        /// <summary>
        /// �ڴ�֮ǰ�����������ƹ��ˡ�
        /// </summary>
        public FilterableClassifier(ValueFilter<TValue> valueFilter, KeyGenerator<TKey, TValue> keyGenerator)
        {
            ValueFilter = valueFilter;
            KeyGenerator = keyGenerator;
        }

        public override bool IsCanonical(TValue value) => ValueFilter != null && ValueFilter.Invoke(value);
        public override TKey GenerateKey(TValue value) => KeyGenerator.Invoke(value);
    }
}
