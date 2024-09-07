using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Traverse
{
    public interface ITraversable
    {
        /// <summary>
        /// �����˶���
        /// </summary>
        void Traverse();
    }

    public interface ITraversable<TTraverser, TEnumerable, TValue> : ITraversable where TTraverser : Traverser<TEnumerable, TValue> where TEnumerable : IEnumerable<TValue>
    {
        TTraverser Traverser { get; set; }
    }



    public interface ITraversable<TEnumerable, TValue> where TEnumerable : IEnumerable<TValue>
    {
        /// <summary>
        /// ����ָ������
        /// </summary>
        void Traverse(TEnumerable values);
    }

    public interface IFlowTraversable<TEnumerable, TValue> : ITraversable<TEnumerable, TValue> where TEnumerable : IEnumerable<TValue>
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
