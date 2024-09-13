using System;
using UnityEngine;

namespace InitialFramework
{
    /// <summary>
    /// 探针。
    /// 
    /// <para>
    /// 本结构用于与 <see cref="IFPhysics"/> 相关的探测计算。
    /// <br>它可以与 <see cref="Ray"/> 进行显式转换。</br>
    /// </para>
    /// </summary>
    [Serializable]
    public struct Probe
    {
        /// <summary>
        /// 探针的原点。
        /// </summary>
        public Vector3 original;
        /// <summary>
        /// 探针的方向。
        /// </summary>
        public Vector3 direction;
        /// <summary>
        /// 探针的半径。
        /// </summary>
        public float radius;

        public Probe(Vector3 original, Vector3 direction, float radius)
        {
            this.original = original;
            this.direction = direction;
            this.radius = radius;
        }

        public readonly Vector3 GetPosition(float distance) => IFMath.MoveTo(original, direction, distance);


        public static Probe ToProbe(Ray ray, float radius) => new(ray.origin, ray.direction, radius);
        public static explicit operator Ray(Probe probe) => new(probe.original, probe.direction);
    }
}
