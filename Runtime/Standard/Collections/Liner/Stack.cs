using System;
using System.Collections.Generic;

namespace FTGAMEStudio.InitialFramework.Collections.Liner
{
    [Serializable]
    /// <summary>
    /// ջ���ϣ�����ģ��ջ����Ϊ��
    /// </summary>
    public class Stack<T> : LinerCollectionBase<T>
    {
        public override int OperateIndex => Count - 1;

        /// <summary>
        /// Ϊջָ��ʹ�õ��б�
        /// </summary>
        public Stack(List<T> values) : base(values) { }
        public Stack() : this(new()) { }
    }
}
