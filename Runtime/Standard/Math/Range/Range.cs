using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Math
{
    /// <summary>
    /// 当您创建一个范围时，请尝试继承本接口。
    /// </summary>
    public interface IRange<T>
    {
        /// <summary>
        /// 钳制传入的值并返回钳制的结果。
        /// </summary>
        public T Clamp(T v);
        /// <summary>
        /// 传入的值是否在范围内。
        /// </summary>
        public bool InRange(T v);
    }

    /// <summary>
    /// 范围基类，用于定义一个范围。
    /// </summary>
    [Serializable]
    public abstract class Range<T> : MinMax<T>, IRange<T> where T : struct
    {
        protected Range(T min, T max) : base(min, max) { }

        public abstract T Clamp(T v);
        public abstract bool InRange(T v);
    }


    /// <summary>
    /// 创建一个 <see cref="float"/> 范围。
    /// 
    /// <para>另请参阅 <seealso cref="Range{T}"/></para>
    /// </summary>
    [Serializable]
    public class FloatRange : Range<float>
    {
        public FloatRange(float min, float max) : base(min, max) { }

        public override float Clamp(float v) => Mathf.Clamp(v, min, max);
        public override bool InRange(float v) => IFMath.InRange(v, min, max);
    }

    /// <summary>
    /// 创建一个 <see cref="Vector2"/> 范围。
    /// 
    /// <para>另请参阅 <seealso cref="Range{T}"/></para>
    /// </summary>
    [Serializable]
    public class Vector2Range : Range<Vector2>
    {
        public Vector2Range(Vector2 min, Vector2 max) : base(min, max) { }

        public override Vector2 Clamp(Vector2 v) => IFMath.Clamp(v, min, max);
        public override bool InRange(Vector2 v) => IFMath.InRange(v, min, max);
    }

    /// <summary>
    /// 创建一个 <see cref="Vector3"/> 范围。
    /// 
    /// <para>另请参阅 <seealso cref="Range{T}"/></para>
    /// </summary>
    [Serializable]
    public class Vector3Range : Range<Vector3>
    {
        public Vector3Range(Vector3 min, Vector3 max) : base(min, max) { }

        public override Vector3 Clamp(Vector3 v) => IFMath.Clamp(v, min, max);
        public override bool InRange(Vector3 v) => IFMath.InRange(v, min, max);
    }
}
