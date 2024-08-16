using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Math.Interpolates
{
    /// <summary>  
    /// 插值接口，用于在两种值（初始值和最终值）之间进行插值。  
    /// </summary>  
    public interface IInterpolation<T> where T : struct
    {
        /// <summary>  
        /// 插值的初始值。  
        /// </summary>  
        T Initial { get; }

        /// <summary>  
        /// 插值的最终值。  
        /// </summary>  
        T Final { get; }
    }

    /// <summary>  
    /// 插值基类。
    /// 
    /// <para>另请参阅 <seealso cref="IInterpolation{T}"/>，<seealso cref="MinMax{T}"/></para>
    /// </summary>  
    [Serializable]
    public abstract class Interpolate<T> : MinMax<T>, IInterpolation<T> where T : struct
    {
        /// <summary>  
        /// 是否反转插值的顺序。如果为true，则插值从Final值到Initial值进行。  
        /// </summary>  
        [Space]
        public bool inverted;

        /// <summary>  
        /// 获取插值的初始值。如果inverted为true，则返回Final值（即max）。  
        /// </summary>  
        public virtual T Initial => inverted ? max : min;

        /// <summary>  
        /// 获取插值的最终值。如果inverted为true，则返回Initial值（即min）。  
        /// </summary>  
        public virtual T Final => inverted ? min : max;

        protected Interpolate() : base() => inverted = false;
        protected Interpolate(T min, T max) : this(min, max, false) { }
        protected Interpolate(T min, T max, bool inverted) : base(min, max) => this.inverted = inverted;
    }
}