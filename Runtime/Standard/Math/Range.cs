using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework
{
    public interface IRange<T>
    {
        T Clamp(T v);
        bool InRange(T v);
    }

    /// <summary>
    /// 范围基类，用于定义一个范围。
    /// </summary>
    [Serializable]
    public abstract class Range<T> : IRange<T>
    {
        public T min;
        public T max;

        protected Range(T min, T max)
        {
            this.min = min;
            this.max = max;
        }

        public abstract T Clamp(T v);
        public abstract bool InRange(T v);
    }


    /// <summary>
    /// 创建一个 <see cref="float"/> 范围。
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
    /// </summary>
    [Serializable]
    public class Vector3Range : Range<Vector3>
    {
        public Vector3Range(Vector3 min, Vector3 max) : base(min, max) { }

        public override Vector3 Clamp(Vector3 v) => IFMath.Clamp(v, min, max);
        public override bool InRange(Vector3 v) => IFMath.InRange(v, min, max);
    }
}
