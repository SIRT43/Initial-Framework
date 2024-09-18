using System.Collections.Generic;

namespace InitialFramework.Traverse
{
    public interface ITraverser<TValue, TContext>
    {
        /// <summary>
        /// ����ָ������
        /// </summary>
        void Traverse(IEnumerable<TValue> values, TContext context);
    }
    public interface ITraverser<TValue> : ITraverser<TValue, object>
    {
        /// <summary>
        /// ����ָ������
        /// </summary>
        void Traverse(IEnumerable<TValue> values);
    }

    public interface IFlowTraverser<TValue, TContext> : ITraverser<TValue, TContext>
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
    public interface IFlowTraverser<TValue> : IFlowTraverser<TValue, object>
    {
        /// <summary>
        /// У�����ֵ�Ƿ�Ϸ���
        /// </summary>
        bool IsCanonical(TValue value);
    }
}
