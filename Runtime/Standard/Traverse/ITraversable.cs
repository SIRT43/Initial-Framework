namespace InitialFramework.Traverse
{
    public interface ITraversable
    {
        /// <summary>
        /// 遍历此对象。
        /// </summary>
        void Traverse();
    }

    public interface ITraversable<TTraverser, TValue, TContext> : ITraversable where TTraverser : Traverser<TValue, TContext>
    {
        TTraverser Traverser { get; set; }
    }
    public interface ITraversable<TTraverser, TValue> : ITraversable<TTraverser, TValue, object> where TTraverser : Traverser<TValue> { }
}
