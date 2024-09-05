namespace FTGAMEStudio.InitialFramework.Classifying
{
    /// <summary>    
    /// 可过滤分类器，它使用提供的委托来过滤和分类值。
    /// 
    /// <para>
    /// <br>这个类为那些需要灵活定义过滤逻辑的场景提供了便利。</br>
    /// <br>使用 <see cref="FilterableClassifier{TKey, TValue}"/> 允许你在不修改类定义的情况下，通过传递不同的过滤规则来改变分类行为。</br>
    /// </para>
    /// </summary>    
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>    
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public class FilterableClassifier<TKey, TValue> : Classifier<TKey, TValue>, IFilterable<TKey, TValue>
    {
        public event ValueFilter<TValue> ValueFilter;
        public event KeyGenerator<TKey, TValue> KeyGenerator;

        /// <summary>
        /// 在此之前，您必须完善过滤。
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
