using System.Collections.Generic;

namespace InitialFramework.Traverse
{
    public interface ITraversable
    {
        /// <summary>
        /// �����˶���
        /// </summary>
        void Traverse();
    }

    public interface ITraversable<TTraverser, TValue, TContext> : ITraversable where TTraverser : Traverser<TValue, TContext>
    {
        TTraverser Traverser { get; set; }
    }



    public interface ITraversable<TValue, TContext>
    {
        /// <summary>
        /// ����ָ������
        /// </summary>
        void Traverse(IEnumerable<TValue> values, TContext context);
    }

    public interface IFlowTraversable<TValue, TContext> : ITraversable<TValue, TContext>
    {
        /// <summary>
        /// ���Ƿ�ʱ��������Ӧ����ô����
        /// 
        /// <para>������� <seealso cref="IsCanonical(TValue, TContext)"/>��</para>
        /// </summary>
        FlowControl NonFlowControl { get; }

        /// <summary>
        /// У�����ֵ�Ƿ�Ϸ���
        /// </summary>
        bool IsCanonical(TValue value, TContext context);
    }
}
