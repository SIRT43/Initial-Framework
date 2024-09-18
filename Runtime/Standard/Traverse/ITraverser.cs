using System.Collections.Generic;

namespace InitialFramework.Traverse
{
    public interface ITraverser<TValue, TContext>
    {
        /// <summary>
        /// 遍历指定对象。
        /// </summary>
        void Traverse(IEnumerable<TValue> values, TContext context);
    }
    public interface ITraverser<TValue> : ITraverser<TValue, object>
    {
        /// <summary>
        /// 遍历指定对象。
        /// </summary>
        void Traverse(IEnumerable<TValue> values);
    }

    public interface IFlowTraverser<TValue, TContext> : ITraverser<TValue, TContext>
    {
        /// <summary>
        /// 当非法时，遍历者应该怎么做。
        /// 
        /// <para>另请参阅 <seealso cref="IsCanonical(TValue, TContext)"/>。</para>
        /// </summary>
        FlowControl NonFlowControl { get; }

        /// <summary>
        /// 校验给定值是否合法。
        /// </summary>
        bool IsCanonical(TValue value, TContext context);
    }
    public interface IFlowTraverser<TValue> : IFlowTraverser<TValue, object>
    {
        /// <summary>
        /// 校验给定值是否合法。
        /// </summary>
        bool IsCanonical(TValue value);
    }
}
