namespace FTGAMEStudio.InitialFramework.Classifying
{
    /// <summary>    
    /// �ɹ��˷���������ʹ���ṩ��ί�������˺ͷ���ֵ��
    /// 
    /// <para>
    /// <br>�����Ϊ��Щ��Ҫ��������߼��ĳ����ṩ�˱�����</br>
    /// <br>���ֱ�Ӽ̳� <see cref="Classifier{TKey, TValue}"/> ����д <see cref="Classifier{TKey, TValue}.IsCanonical(TValue, out TKey)"/> ������</br>
    /// <br>ʹ�� <see cref="FilterableClassifier{TKey, TValue}"/> �������ڲ��޸��ඨ�������£�ͨ�����ݲ�ͬ�Ĺ��˹������ı������Ϊ��</br>
    /// </para>
    /// </summary>    
    /// <typeparam name="TKey">���ڷ���ļ������͡�</typeparam>    
    /// <typeparam name="TValue">Ҫ�����ֵ�����͡�</typeparam>
    public class FilterableClassifier<TKey, TValue> : Classifier<TKey, TValue>, IFilterable<TKey, TValue>
    {
        public virtual FilterRule<TKey, TValue> FilterRule { get; protected set; }

        /// <summary>
        /// �������ṩһ�����˹���
        /// </summary>
        public FilterableClassifier(FilterRule<TKey, TValue> filter) => FilterRule = filter;

        public override bool IsCanonical(TValue value, out TKey key)
        {
            bool result = FilterRule.Invoke(value, out TKey k);

            key = k;
            return result;
        }
    }
}
