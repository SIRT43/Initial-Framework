using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Math.Interpolates
{
    /// <summary>  
    /// ��ֵ�ӿڣ�����������ֵ����ʼֵ������ֵ��֮����в�ֵ��  
    /// </summary>  
    public interface IInterpolation<T> where T : struct
    {
        /// <summary>  
        /// ��ֵ�ĳ�ʼֵ��  
        /// </summary>  
        T Initial { get; }

        /// <summary>  
        /// ��ֵ������ֵ��  
        /// </summary>  
        T Final { get; }
    }

    /// <summary>  
    /// ��ֵ���ࡣ
    /// 
    /// <para>������� <seealso cref="IInterpolation{T}"/>��<seealso cref="MinMax{T}"/></para>
    /// </summary>  
    [Serializable]
    public abstract class Interpolate<T> : MinMax<T>, IInterpolation<T> where T : struct
    {
        /// <summary>  
        /// �Ƿ�ת��ֵ��˳�����Ϊtrue�����ֵ��Finalֵ��Initialֵ���С�  
        /// </summary>  
        [Space]
        public bool inverted;

        /// <summary>  
        /// ��ȡ��ֵ�ĳ�ʼֵ�����invertedΪtrue���򷵻�Finalֵ����max����  
        /// </summary>  
        public virtual T Initial => inverted ? max : min;

        /// <summary>  
        /// ��ȡ��ֵ������ֵ�����invertedΪtrue���򷵻�Initialֵ����min����  
        /// </summary>  
        public virtual T Final => inverted ? min : max;

        protected Interpolate() : base() => inverted = false;
        protected Interpolate(T min, T max) : this(min, max, false) { }
        protected Interpolate(T min, T max, bool inverted) : base(min, max) => this.inverted = inverted;
    }
}