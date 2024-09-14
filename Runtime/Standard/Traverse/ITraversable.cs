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

    public interface ITraversable<TTraverser, TValue> : ITraversable where TTraverser : Traverser<TValue>
    {
        TTraverser Traverser { get; set; }
    }



    public interface ITraversable<TValue>
    {
        /// <summary>
        /// ����ָ������
        /// </summary>
        void Traverse(IEnumerable<TValue> values);
    }

    public interface IFlowTraversable<TValue> : ITraversable<TValue>
    {
        /// <summary>
        /// ���Ƿ�ʱ��������Ӧ����ô����
        /// 
        /// <para>������� <seealso cref="IsCanonical(TValue)"/>��</para>
        /// </summary>
        FlowControl NonFlowControl { get; }

        /// <summary>
        /// У�����ֵ�Ƿ�Ϸ���
        /// </summary>
        bool IsCanonical(TValue value);
    }
}
