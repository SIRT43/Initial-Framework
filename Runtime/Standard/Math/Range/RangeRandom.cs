using System;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Math
{
    /// <summary>
    /// 可随机的。
    /// <br>当您创建可随机对象时，请继承本接口。</br>
    /// </summary>
    public interface IRandomable<T> : IRange<T>
    {
        public T Range();
    }

    [Serializable]
    public class FloatRandom : FloatRange, IRandomable<float>
    {
        public FloatRandom(float min, float max) : base(min, max) { }

        public float Range() => UnityEngine.Random.Range(min, max);
    }

    [Serializable]
    public class Vector2Random : Vector2Range, IRandomable<Vector2>
    {
        public Vector2Random(Vector2 min, Vector2 max) : base(min, max) { }

        public Vector2 Range() => IFRandom.Range(min, max);
    }

    [Serializable]
    public class Vector3Random : Vector3Range
    {
        public Vector3Random(Vector3 min, Vector3 max) : base(min, max) { }

        public Vector3 Range() => IFRandom.Range(min, max);
    }
}
