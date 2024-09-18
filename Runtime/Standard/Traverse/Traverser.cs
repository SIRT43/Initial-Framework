using System.Collections.Generic;

namespace InitialFramework.Traverse
{
    public abstract class Traverser<TValue, TContext> : IFlowTraverser<TValue, TContext>
    {
        public virtual FlowControl NonFlowControl { get; }


        protected Traverser(FlowControl nonFlowControl) => NonFlowControl = nonFlowControl;


        public virtual void Traverse(IEnumerable<TValue> values, TContext context)
        {
            BeforeTraverse(values, context);

            foreach (TValue value in values)
            {
                if (!IsCanonical(value, context))
                {
                    OnNonCanonical(value, context);

                    if (NonFlowControl == FlowControl.Return)
                    {
                        AfterTraverse(values, context);
                        return;
                    }
                    if (NonFlowControl == FlowControl.Break) break;
                    if (NonFlowControl == FlowControl.Continue) continue;
                }

                OnTraverse(value, context);
            }

            AfterTraverse(values, context);
        }

        /// <summary>
        /// 校验给定值是否合法，本方法默认返回 true。
        /// </summary>
        public virtual bool IsCanonical(TValue value, TContext context) => true;


        /// <summary>
        /// 当遍历遇到非法值时将调用此方法。
        /// 
        /// <para>另请参阅 <seealso cref="IsCanonical(TValue, TContext)"/></para>
        /// </summary>
        protected virtual void OnNonCanonical(TValue value, TContext context) { }


        /// <summary>
        /// 当遍历开始前调用此方法。
        /// </summary>
        protected virtual void BeforeTraverse(IEnumerable<TValue> values, TContext context) { }
        /// <summary>
        /// 当遍历结束后将调用此方法。
        ///
        /// <para>请注意，本方法也将在 <see cref="NonFlowControl"/> 为 <see cref="FlowControl.Return"/> 或 <see cref="FlowControl.Break"/> 时调用。</para>
        /// </summary>
        protected virtual void AfterTraverse(IEnumerable<TValue> values, TContext context) { }

        /// <summary>  
        /// 在遍历过程中，对每一个通过 <see cref="IsCanonical(TValue, TContext)"/> 校验的合法值执行操作。  
        /// </summary>
        protected abstract void OnTraverse(TValue value, TContext context);
    }

    public abstract class Traverser<TValue> : Traverser<TValue, object>, IFlowTraverser<TValue>
    {
        protected Traverser(FlowControl nonFlowControl) : base(nonFlowControl) { }

        public virtual void Traverse(IEnumerable<TValue> values) => Traverse(values, null);
        public override bool IsCanonical(TValue value, object context) => IsCanonical(value);
        protected override void OnNonCanonical(TValue value, object context) => OnNonCanonical(value);
        protected override void BeforeTraverse(IEnumerable<TValue> values, object context) => BeforeTraverse(values);
        protected override void AfterTraverse(IEnumerable<TValue> values, object context) => AfterTraverse(values);
        protected override void OnTraverse(TValue value, object context) => OnTraverse(value);

        /// <summary>
        /// 校验给定值是否合法，本方法默认返回 true。
        /// </summary>
        public virtual bool IsCanonical(TValue value) => true;

        /// <summary>
        /// 当遍历遇到非法值时将调用此方法。
        /// 
        /// <para>另请参阅 <seealso cref="IsCanonical(TValue)"/></para>
        /// </summary>
        protected virtual void OnNonCanonical(TValue value) { }

        /// <summary>
        /// 当遍历开始前调用此方法。
        /// </summary>
        protected virtual void BeforeTraverse(IEnumerable<TValue> values) { }
        /// <summary>
        /// 当遍历结束后将调用此方法。
        ///
        /// <para>请注意，本方法也将在 <see cref="NonFlowControl"/> 为 <see cref="FlowControl.Return"/> 或 <see cref="FlowControl.Break"/> 时调用。</para>
        /// </summary>
        protected virtual void AfterTraverse(IEnumerable<TValue> values) { }

        /// <summary>  
        /// 在遍历过程中，对每一个通过 <see cref="IsCanonical(TValue)"/> 校验的合法值执行操作。  
        /// </summary>
        protected abstract void OnTraverse(TValue value);
    }
}
