using System;
using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Collections.Liner
{
    /// <summary>
    /// 队列，实现了队列行为的集合。
    /// </summary>
    [Serializable]
    public class Queue<T> : LinerCollectionBase<T>
    {
        public override int OperateIndex => 0;

        /// <summary>
        /// 为队列指定使用的列表。
        /// </summary>
        public Queue(List<T> values) : base(values) { }
        public Queue() : this(new()) { }
    }
}
