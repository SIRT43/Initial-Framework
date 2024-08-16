using FTGAMEStudio.InitialFramework.Math;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>
    /// ̽�롣
    /// 
    /// <para>
    /// ���ṹ������ <see cref="IFPhysics"/> ��ص�̽����㡣
    /// <br>�������� <see cref="Ray"/> ������ʽת�������� <see cref="Ray"/> ת��Ϊ <see cref="Probe"/> ����ʧ�뾶���ݡ�</br>
    /// </para>
    /// </summary>
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

        public static explicit operator Ray(Probe probe) => new(probe.original, probe.direction);
        public static explicit operator Probe(Ray ray) => new(ray.origin, ray.direction, 1);
    }

    public readonly struct DetectionResult
    {
        /// <summary>
        /// ̽�����һ��δ������ײ��Բ�ĵ㡣
        /// </summary>
        public readonly Vector3 position;
        /// <summary>
        /// ԭ�㵽Բ�ĵ�ľ��롣
        /// </summary>
        public readonly float distance;

        public DetectionResult(Vector3 position, float distance)
        {
            this.position = position;
            this.distance = distance;
        }

        public DetectionResult(Vector3 position, Vector3 original) :
            this(position, Vector3.Distance(original, position))
        { }

        public DetectionResult(Vector3 original, Vector3 direction, float distance)
        {
            Vector3 maxPosition = IFMath.MoveTo(original, direction, distance);

            position = maxPosition;
            this.distance = distance;
        }

        public DetectionResult(Probe probe, float distance) : this(probe.original, probe.direction, distance) { }
    }

    /// <summary>
    /// ������� <see cref="Physics"/> ��װ�ṩ�˸��෽����
    /// </summary>
    public static class IFPhysics
    {
        /// <summary>  
        /// ��ָ������Ͷ��̽�룬̽�ⲻ������ײ����Զλ�ü�����루λ����̽���Բ�ģ�������λ�õ�ԭ�㣩��  
        ///   
        /// <para>
        /// ������� <seealso cref="Physics.SphereCast"/> �� <seealso cref="Physics.CheckSphere"/>��
        /// </para>  
        /// </summary>  
        /// <param name="maxDistance">̽��Ͷ��������롣</param>  
        public static DetectionResult Detection(Probe probe, float maxDistance) =>
            Detection(probe, maxDistance, 1);

        /// <summary>  
        /// ��ָ������Ͷ��̽�룬̽�ⲻ������ײ����Զλ�ü�����루λ����̽���Բ�ģ�������λ�õ�ԭ�㣩��  
        ///   
        /// <para>
        /// ������� <seealso cref="Physics.SphereCast"/> �� <seealso cref="Physics.CheckSphere"/>��
        /// </para>  
        /// </summary>  
        /// <param name="maxDistance">̽��Ͷ��������롣</param>  
        /// <param name="density">̽��·���ϲ��Ե���ܶȡ�ֵԽ�ߣ����Ե�Խ�࣬����Խ�ߣ���������ҲԽ��</param>  
        public static DetectionResult Detection(Probe probe, float maxDistance, float density) =>
            Detection(probe, maxDistance, density, 0.5f);

        /// <summary>  
        /// ��ָ������Ͷ��̽�룬̽�ⲻ������ײ����Զλ�ü�����루λ����̽���Բ�ģ�������λ�õ�ԭ�㣩��  
        ///   
        /// <para>
        /// ������� <seealso cref="Physics.SphereCast"/> �� <seealso cref="Physics.CheckSphere"/>��
        /// </para>  
        /// </summary>  
        /// <param name="maxDistance">̽��Ͷ��������롣</param>  
        /// <param name="density">̽��·���ϲ��Ե���ܶȡ�ֵԽ�ߣ����Ե�Խ�࣬����Խ�ߣ���������ҲԽ��</param>  
        /// <param name="inference">�Ʋⲻ������ײ����Զλ��ʱ�ľ���������ӡ�ֵԽ�ߣ��Ʋ��λ��Լ�ӽ���Զλ�ã��Ʋ�Ľ���ľ���Խ�ͣ���������ԽС��</param>  
        public static DetectionResult Detection(Probe probe, float maxDistance, float density, float inference) =>
            Detection(probe, maxDistance, density, inference, ~4);

        /// <summary>  
        /// ��ָ������Ͷ��̽�룬̽�ⲻ������ײ����Զλ�ü�����루λ����̽���Բ�ģ�������λ�õ�ԭ�㣩��  
        ///   
        /// <para>
        /// ������� <seealso cref="Physics.SphereCast"/> �� <seealso cref="Physics.CheckSphere"/>��
        /// </para>  
        /// </summary>  
        /// <param name="maxDistance">̽��Ͷ��������롣</param>  
        /// <param name="density">̽��·���ϲ��Ե���ܶȡ�ֵԽ�ߣ����Ե�Խ�࣬����Խ�ߣ���������ҲԽ��</param>  
        public static DetectionResult Detection(Probe probe, float maxDistance, float density, LayerMask targetLayer) =>
            Detection(probe, maxDistance, density, 0.5f, targetLayer);

        /// <summary>  
        /// ��ָ������Ͷ��̽�룬̽�ⲻ������ײ����Զλ�ü�����루λ����̽���Բ�ģ�������λ�õ�ԭ�㣩��  
        ///   
        /// <para>
        /// ������� <seealso cref="Physics.SphereCast"/> �� <seealso cref="Physics.CheckSphere"/>��
        /// </para>  
        /// </summary>  
        /// <param name="maxDistance">̽��Ͷ��������롣</param>  
        /// <param name="density">̽��·���ϲ��Ե���ܶȡ�ֵԽ�ߣ����Ե�Խ�࣬����Խ�ߣ���������ҲԽ��</param>  
        /// <param name="inference">�Ʋⲻ������ײ����Զλ��ʱ�ľ���������ӡ�ֵԽ�ߣ��Ʋ��λ��Լ�ӽ���Զλ�ã��Ʋ�Ľ���ľ���Խ�ͣ���������ԽС��</param>  
        public static DetectionResult Detection(Probe probe, float maxDistance, float density, float inference, LayerMask targetLayer)
        {
            if (!Physics.SphereCast((Ray)probe, probe.radius, out RaycastHit hitInfo, maxDistance, targetLayer))
                return new(probe, maxDistance);

            Vector3 original = IFMath.MoveTo(probe.original, probe.direction, hitInfo.distance * inference);

            float testPosCount = maxDistance / probe.radius * density;
            float singleDistance = maxDistance / testPosCount;

            Vector3 noCollision = FindNoCollisionPosition(original, probe.direction, probe.radius, singleDistance, maxDistance, targetLayer);

            return new(noCollision, probe.original);
        }

        /// <summary>
        /// ������ָ���������ָ��������뾶�����ɴ�㡣
        /// <br>���Ƽ�ʹ�ô˷�������ʹ�� <see cref="Detection"/>��</br>
        /// </summary>
        /// <param name="original">����ԭ�㡣</param>
        /// <param name="direction">���ҷ���</param>
        /// <param name="radius">������뾶��</param>
        /// <param name="singleDistance">ÿ����һ�����ӵľ��롣</param>
        public static Vector3 FindNoCollisionPosition(
            Vector3 original,
            Vector3 direction,
            float radius,
            float singleDistance,
            float maxDistance,
            LayerMask targetLayer)
        {
            Vector3 noCollision = original;

            for (float currentDistance = singleDistance; currentDistance <= maxDistance; currentDistance += singleDistance)
            {
                Vector3 testPosition = IFMath.MoveTo(original, direction, currentDistance);

                if (!Physics.CheckSphere(testPosition, radius, targetLayer)) noCollision = testPosition;
                else break;
            }

            return noCollision;
        }
    }
}
