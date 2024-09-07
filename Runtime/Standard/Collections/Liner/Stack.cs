using System;
using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Collections.Liner
{
    [Serializable]
    /// <summary>
    /// 栈集合，用于模拟栈的行为。
    /// </summary>
    public class Stack<T> : LinerCollectionBase<T>
    {
        public override int OperateIndex => Count - 1;

        /// <summary>
        /// 为栈指定使用的列表。
        /// </summary>
        public Stack(List<T> values) : base(values) { }
        public Stack() : this(new()) { }
    }
}
