using System.Collections.Generic;

namespace InitialFramework.Traverse
{
    public abstract class Traverser<TValue, TContext> : IFlowTraversable<TValue, TContext>
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
        /// У�����ֵ�Ƿ�Ϸ���������Ĭ�Ϸ��� true��
        /// </summary>
        public virtual bool IsCanonical(TValue value, TContext context) => true;


        /// <summary>
        /// �����������Ƿ�ֵʱ�����ô˷�����
        /// 
        /// <para>������� <seealso cref="IsCanonical(TValue, TContext)"/></para>
        /// </summary>
        protected virtual void OnNonCanonical(TValue value, TContext context) { }


        /// <summary>
        /// ��������ʼǰ���ô˷�����
        /// </summary>
        protected virtual void BeforeTraverse(IEnumerable<TValue> values, TContext context) { }
        /// <summary>
        /// �����������󽫵��ô˷�����
        ///         
        /// <para>��ע�⣬������Ҳ���� <see cref="NonFlowControl"/> Ϊ <see cref="FlowControl.Return"/> ʱ���á�</para>
        /// </summary>
        protected virtual void AfterTraverse(IEnumerable<TValue> values, TContext context) { }

        /// <summary>  
        /// �ڱ��������У���ÿһ��ͨ�� <see cref="IsCanonical(TValue, TContext)"/> У��ĺϷ�ִֵ�в�����  
        /// </summary>
        protected abstract void OnTraverse(TValue value, TContext context);
    }
}
