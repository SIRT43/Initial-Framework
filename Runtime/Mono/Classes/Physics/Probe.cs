using System;
using UnityEngine;

namespace InitialFramework
{
    /// <summary>
    /// ̽�롣
    /// 
    /// <para>
    /// ���ṹ������ <see cref="IFPhysics"/> ��ص�̽����㡣
    /// <br>�������� <see cref="Ray"/> ������ʽת����</br>
    /// </para>
    /// </summary>
    [Serializable]
    public struct Probe
    {
        /// <summary>
        /// ̽���ԭ�㡣
        /// </summary>
        public Vector3 original;
        /// <summary>
        /// ̽��ķ���
        /// </summary>
        public Vector3 direction;
        /// <summary>
        /// ̽��İ뾶��
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
