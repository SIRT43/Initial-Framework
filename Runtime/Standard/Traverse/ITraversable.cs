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

    public interface ITraversable<TValue, TEnumerable, TTraverser> : ITraversable where TTraverser : Traverser<TValue, TEnumerable> where TEnumerable : IEnumerable<TValue>
    {
        TTraverser Traverser { get; set; }
    }


    public interface ITraversable<TValue, TEnumerable> where TEnumerable : IEnumerable<TValue>
    {
        /// <summary>
        /// ����ָ������
        /// </summary>
        void Traverse(TEnumerable values);
    }


    public interface IFlowTraversable<TValue, TEnumerable> : ITraversable<TValue, TEnumerable> where TEnumerable : IEnumerable<TValue>
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
