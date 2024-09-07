using System;
using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Collections.Liner
{
    /// <summary>
    /// ���У�ʵ���˶�����Ϊ�ļ��ϡ�
    /// </summary>
    [Serializable]
    public class Queue<T> : LinerCollectionBase<T>
    {
        public override int OperateIndex => 0;

        /// <summary>
        /// Ϊ����ָ��ʹ�õ��б�
        /// </summary>
        public Queue(List<T> values) : base(values) { }
        public Queue() : this(new()) { }
    }
}
