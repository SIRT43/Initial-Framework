using System;
using System.Collections.Generic;

namespace InitialFramework.Traverse
{
    /// <summary>
    /// ����������������ί���¼��������¼���
    /// </summary>
    public class BuffetTraverser<TValue, TContext> : Traverser<TValue, TContext>, IFlowTraversable<TValue, TContext>
    {
        public Action<TValue, TContext> onTraverse;
        public Func<TValue, TContext, bool> isCanonical;

        public Action<TValue, TContext> onNonCanonical;

        public Action<IEnumerable<TValue>, TContext> afterTraverse;
        public Action<IEnumerable<TValue>, TContext> beforeTraverse;

        public BuffetTraverser(Action<TValue, TContext> onTraverse,
                               Func<TValue, TContext, bool> isCanonical = null,
                               FlowControl nonFlowControl = FlowControl.Continue,
                               Action<IEnumerable<TValue>, TContext> afterTraverse = null,
                               Action<IEnumerable<TValue>, TContext> beforeTraverse = null,
                               Action<TValue, TContext> onNonCanonical = null) : base(nonFlowControl)
        {
            this.onTraverse = onTraverse ?? throw new ArgumentNullException(nameof(onTraverse), "For this parameter, you must provide a non-null value. (onTraverse)");

            this.isCanonical = isCanonical;
            this.onNonCanonical = onNonCanonical;
            this.afterTraverse = afterTraverse;
            this.beforeTraverse = beforeTraverse;
        }

        protected override void OnTraverse(TValue value, TContext context) => onTraverse?.Invoke(value, context);
        public override bool IsCanonical(TValue value, TContext context) => isCanonical == null || isCanonical.Invoke(value, context);


        protected override void OnNonCanonical(TValue value, TContext context) => onNonCanonical?.Invoke(value, context);

        protected override void AfterTraverse(IEnumerable<TValue> values, TContext context) => afterTraverse?.Invoke(values, context);
        protected override void BeforeTraverse(IEnumerable<TValue> values, TContext context) => beforeTraverse?.Invoke(values, context);
    }
}
