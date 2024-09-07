using System;
using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Traverse
{
    /// <summary>
    /// 自助遍历器，基于委托事件处理方法事件。
    /// </summary>
    public class BuffetTraverser<TEnumerable, TValue> : Traverser<TEnumerable, TValue>, IFlowTraversable<TEnumerable, TValue> where TEnumerable : IEnumerable<TValue>
    {
        public Action<TValue> onTraverse;
        public Func<TValue, bool> isCanonical;

        public Action<TValue> onNonCanonical;

        public Action<TEnumerable> afterTraverse;
        public Action<TEnumerable> beforeTraverse;

        public BuffetTraverser(Action<TValue> onTraverse,
                               Func<TValue, bool> isCanonical = null,
                               FlowControl nonFlowControl = FlowControl.Continue,
                               Action<TEnumerable> afterTraverse = null,
                               Action<TEnumerable> beforeTraverse = null,
                               Action<TValue> onNonCanonical = null) : base(nonFlowControl)
        {
            this.onTraverse = onTraverse ?? throw new ArgumentNullException(nameof(onTraverse), "For this parameter, you must provide a non-null value. (onTraverse)");

            this.isCanonical = isCanonical;
            this.onNonCanonical = onNonCanonical;
            this.afterTraverse = afterTraverse;
            this.beforeTraverse = beforeTraverse;
        }

        protected override void OnTraverse(TValue value) => onTraverse?.Invoke(value);
        public override bool IsCanonical(TValue value) => isCanonical == null || isCanonical.Invoke(value);


        protected override void OnNonCanonical(TValue value) => onNonCanonical?.Invoke(value);

        protected override void AfterTraverse(TEnumerable values) => afterTraverse?.Invoke(values);
        protected override void BeforeTraverse(TEnumerable values) => beforeTraverse?.Invoke(values);
    }
}
