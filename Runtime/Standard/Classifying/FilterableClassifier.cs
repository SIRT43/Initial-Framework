namespace FTGAMEStudio.InitialFramework.Classifying
{
    /// <summary>    
    /// 可过滤分类器，它使用提供的委托来过滤和分类值。
    /// 
    /// <para>
    /// <br>这个类为那些需要灵活定义过滤逻辑的场景提供了便利。</br>
    /// <br>相比直接继承 <see cref="Classifier{TKey, TValue}"/> 并重写 <see cref="Classifier{TKey, TValue}.IsCanonical(TValue, out TKey)"/> 方法，</br>
    /// <br>使用 <see cref="FilterableClassifier{TKey, TValue}"/> 允许你在不修改类定义的情况下，通过传递不同的过滤规则来改变分类行为。</br>
    /// </para>
    /// </summary>    
    /// <typeparam name="TKey">用于分类的键的类型。</typeparam>    
    /// <typeparam name="TValue">要分类的值的类型。</typeparam>
    public class FilterableClassifier<TKey, TValue> : Classifier<TKey, TValue>, IFilterable<TKey, TValue>
    {
        public virtual FiltrationRule<TKey, TValue> Filter { get; protected set; }

        /// <summary>
        /// 您必须提供一个过滤规则。
        /// </summary>
        public FilterableClassifier(FiltrationRule<TKey, TValue> filter) => Filter = filter;

        public override bool IsCanonical(TValue value, out TKey key)
        {
            bool result = Filter.Invoke(value, out TKey k);

            key = k;
            return result;
        }
    }
}
