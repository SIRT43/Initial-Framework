using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Math
{
    /// <summary>
    /// ��������һ����Χʱ���볢�Լ̳б��ӿڡ�
    /// </summary>
    public interface IRange<T>
    {
        /// <summary>
        /// ǯ�ƴ����ֵ������ǯ�ƵĽ����
        /// </summary>
        public T Clamp(T v);
        /// <summary>
        /// �����ֵ�Ƿ��ڷ�Χ�ڡ�
        /// </summary>
        public bool InRange(T v);
    }

    /// <summary>
    /// ��Χ���࣬���ڶ���һ����Χ��
    /// </summary>
    [Serializable]
    public abstract class Range<T> : MinMax<T>, IRange<T> where T : struct
    {
        protected Range(T min, T max) : base(min, max) { }

        public abstract T Clamp(T v);
        public abstract bool InRange(T v);
    }


    /// <summary>
    /// ����һ�� <see cref="float"/> ��Χ��
    /// 
    /// <para>������� <seealso cref="Range{T}"/></para>
    /// </summary>
    [Serializable]
    public class FloatRange : Range<float>
    {
        public FloatRange(float min, float max) : base(min, max) { }

        public override float Clamp(float v) => Mathf.Clamp(v, min, max);
        public override bool InRange(float v) => IFMath.InRange(v, min, max);
    }

    /// <summary>
    /// ����һ�� <see cref="Vector2"/> ��Χ��
    /// 
    /// <para>������� <seealso cref="Range{T}"/></para>
    /// </summary>
    [Serializable]
    public class Vector2Range : Range<Vector2>
    {
        public Vector2Range(Vector2 min, Vector2 max) : base(min, max) { }

        public override Vector2 Clamp(Vector2 v) => IFMath.Clamp(v, min, max);
        public override bool InRange(Vector2 v) => IFMath.InRange(v, min, max);
    }

    /// <summary>
    /// ����һ�� <see cref="Vector3"/> ��Χ��
    /// 
    /// <para>������� <seealso cref="Range{T}"/></para>
    /// </summary>
    [Serializable]
    public class Vector3Range : Range<Vector3>
    {
        public Vector3Range(Vector3 min, Vector3 max) : base(min, max) { }

        public override Vector3 Clamp(Vector3 v) => IFMath.Clamp(v, min, max);
        public override bool InRange(Vector3 v) => IFMath.InRange(v, min, max);
    }
}
