using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Traverse
{
    public interface ITraversable
    {
        /// <summary>
        /// 遍历此对象。
        /// </summary>
        void Traverse();
    }

    public interface ITraversable<TValue, TEnumerable, TTraverser> : ITraversable where TTraverser : Traverser<TValue, TEnumerable> where TEnumerable : IEnumerable<TValue>
    {
        TTraverser Traverser { get; set; }
    }


    public interface ITraversable<TValue, TEnumerable> where TEnumerable : IEnumerable<TValue>
    {
        /// <summary>
        /// 遍历指定对象。
        /// </summary>
        void Traverse(TEnumerable values);
    }


    public interface IFlowTraversable<TValue, TEnumerable> : ITraversable<TValue, TEnumerable> where TEnumerable : IEnumerable<TValue>
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
