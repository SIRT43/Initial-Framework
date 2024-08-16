using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Math
{
    /// <summary>  
    /// 分数，用于处理分数相关的计算。  
    /// <br>分数由分子 <see cref="numerator"/> 和分母 <see cref="denominator"/> 组成，可以隐式转换为浮点数以获取分数的小数表示。</br>  
    /// </summary>  
    [Serializable]
    public struct Fraction
    {
        /// <summary>
        /// 分子。
        /// </summary>
        public float numerator;
        /// <summary>
        /// 分母。
        /// </summary>
        public float denominator;

        public readonly float Result => numerator / denominator;
        public readonly Quotient Division => new(Mathf.FloorToInt(Result), IFMath.FloorPart(Result));


        public override readonly string ToString() => $"{numerator} / {denominator}";

        public static implicit operator float(Fraction fraction) => fraction.Result;
    }

    /// <summary>  
    /// 除法结果，包含整数部分 <see cref="integer"/> 和小数部分 <see cref="remainder"/>，以及完整的浮点数表示 <see cref="full"/>。  
    /// <br>该结构常用于表示除法操作的结果，特别是当需要同时访问整数部分和小数部分时。</br>  
    /// </summary>  
    public readonly struct Quotient
    {
        public readonly int integer;
        public readonly float remainder;

        public readonly float full;

        public Quotient(int integer, float remainder)
        {
            this.integer = integer;
            this.remainder = remainder;

            full = this.integer + this.remainder;
        }


        public override string ToString() => full.ToString();

        public static implicit operator float(Quotient quotient) => quotient.full;
    }
}
