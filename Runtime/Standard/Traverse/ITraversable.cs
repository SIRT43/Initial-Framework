namespace InitialFramework.Traverse
{
    public interface ITraversable
    {
        /// <summary>
        /// �����˶���
        /// </summary>
        void Traverse();
    }

    public interface ITraversable<TTraverser, TValue, TContext> : ITraversable where TTraverser : Traverser<TValue, TContext>
    {
        TTraverser Traverser { get; set; }
    }
    public interface ITraversable<TTraverser, TValue> : ITraversable<TTraverser, TValue, object> where TTraverser : Traverser<TValue> { }
}
