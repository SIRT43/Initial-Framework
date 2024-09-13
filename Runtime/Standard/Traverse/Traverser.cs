using System.Collections.Generic;

namespace InitialFramework.Traverse
{
    public abstract class Traverser<TEnumerable, TValue> : IFlowTraversable<TEnumerable, TValue> where TEnumerable : IEnumerable<TValue>
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
        /// У�����ֵ�Ƿ�Ϸ���������Ĭ�Ϸ��� true��
        /// </summary>
        public virtual bool IsCanonical(TValue value) => true;


        /// <summary>
        /// �����������Ƿ�ֵʱ�����ô˷�����
        /// 
        /// <para>������� <seealso cref="IsCanonical(TValue)"/></para>
        /// </summary>
        protected virtual void OnNonCanonical(TValue value) { }


        /// <summary>
        /// ��������ʼǰ���ô˷�����
        /// </summary>
        protected virtual void BeforeTraverse(TEnumerable values) { }
        /// <summary>
        /// �����������󽫵��ô˷�����
        ///         
        /// <para>��ע�⣬������Ҳ���� <see cref="NonFlowControl"/> Ϊ <see cref="FlowControl.Return"/> ʱ���á�</para>
        /// </summary>
        protected virtual void AfterTraverse(TEnumerable values) { }

        /// <summary>  
        /// �ڱ��������У���ÿһ��ͨ�� <see cref="IsCanonical(TValue)"/> У��ĺϷ�ִֵ�в�����  
        /// </summary>
        protected abstract void OnTraverse(TValue value);
    }
}
