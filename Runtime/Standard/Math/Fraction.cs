using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Math
{
    /// <summary>  
    /// ���������ڴ��������صļ��㡣  
    /// <br>�����ɷ��� <see cref="numerator"/> �ͷ�ĸ <see cref="denominator"/> ��ɣ�������ʽת��Ϊ�������Ի�ȡ������С����ʾ��</br>  
    /// </summary>  
    [Serializable]
    public struct Fraction
    {
        /// <summary>
        /// ���ӡ�
        /// </summary>
        public float numerator;
        /// <summary>
        /// ��ĸ��
        /// </summary>
        public float denominator;

        public readonly float Result => numerator / denominator;
        public readonly Quotient Division => new(Mathf.FloorToInt(Result), IFMath.FloorPart(Result));


        public override readonly string ToString() => $"{numerator} / {denominator}";

        public static implicit operator float(Fraction fraction) => fraction.Result;
    }

    /// <summary>  
    /// ��������������������� <see cref="integer"/> ��С������ <see cref="remainder"/>���Լ������ĸ�������ʾ <see cref="full"/>��  
    /// <br>�ýṹ�����ڱ�ʾ���������Ľ�����ر��ǵ���Ҫͬʱ�����������ֺ�С������ʱ��</br>  
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
