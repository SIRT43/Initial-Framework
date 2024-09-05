using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Traverse
{
    public abstract class Traverser<TValue, TEnumerable> : IFlowTraversable<TValue, TEnumerable> where TEnumerable : IEnumerable<TValue>
    {
        public virtual FlowControl NonFlowControl { get; }


        protected Traverser(FlowControl nonFlowControl) => NonFlowControl = nonFlowControl;


        public virtual void Traverse(TEnumerable values)
        {
            BeforeTraverse(values);

            foreach (TValue value in values)
            {
                if (!IsCanonical(value))
                {
                    OnNonCanonical(value);

                    if (NonFlowControl == FlowControl.Return)
                    {
                        AfterTraverse(values);
                        return;
                    }
                    if (NonFlowControl == FlowControl.Break) break;
                    if (NonFlowControl == FlowControl.Continue) continue;
                }

                OnTraverse(value);
            }

            AfterTraverse(values);
        }

        /// <summary>
        /// 校验给定值是否合法，本方法默认返回 true。
        /// </summary>
        public virtual bool IsCanonical(TValue value) => true;


        /// <summary>
        /// 当遍历遇到非法值时将调用此方法。
        /// 
        /// <para>另请参阅 <seealso cref="IsCanonical(T)"/></para>
        /// </summary>
        protected virtual void OnNonCanonical(TValue value) { }


        /// <summary>
        /// 当遍历开始前调用此方法。
        /// </summary>
        protected virtual void BeforeTraverse(TEnumerable values) { }
        /// <summary>
        /// 当遍历结束后将调用此方法。
        ///         
        /// <para>请注意，本方法也将在 <see cref="NonFlowControl"/> 为 <see cref="FlowControl.Return"/> 时调用。</para>
        /// </summary>
        protected virtual void AfterTraverse(TEnumerable values) { }

        /// <summary>
        /// 当遍历时。
        /// 
        /// <para>值被视为非法时将不调用此方法。</para>
        /// </summary>
        protected abstract void OnTraverse(TValue value);
    }
}
