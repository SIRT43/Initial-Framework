using System.Collections.Generic;

namespace InitialFramework.Traverse
{
    public interface ITraversable
    {
        /// <summary>
        /// 遍历此对象。
        /// </summary>
        void Traverse();
    }

    public interface ITraversable<TTraverser, TValue> : ITraversable where TTraverser : Traverser<TValue>
    {
        TTraverser Traverser { get; set; }
    }



    public interface ITraversable<TValue>
    {
        /// <summary>
        /// 遍历指定对象。
        /// </summary>
        void Traverse(IEnumerable<TValue> values);
    }

    public interface IFlowTraversable<TValue> : ITraversable<TValue>
    {
        /// <summary>
        /// 当非法时，遍历者应该怎么做。
        /// 
        /// <para>另请参阅 <seealso cref="IsCanonical(TValue)"/>。</para>
        /// </summary>
        FlowControl NonFlowControl { get; }

        /// <summary>
        /// 校验给定值是否合法。
        /// </summary>
        bool IsCanonical(TValue value);
    }
}
