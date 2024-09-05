using UnityEngine;

namespace FTGAMEStudio.InitialFramework
{
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
        /// <param name="density">̽��·���ϲ��Ե���ܶȡ�ֵԽ�ߣ����Ե�Խ�࣬����Խ�ߣ���������ҲԽ��</param>  
        /// <param name="inference">�Ʋⲻ������ײ����Զλ��ʱ�ľ���������ӡ�ֵԽ�ߣ��Ʋ��λ��Լ�ӽ���Զλ�ã��Ʋ�Ľ������ȷ��Խ�ͣ���������ԽС��</param>  
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
        /// ��ָ������Ͷ��̽�룬̽�ⲻ������ײ����Զλ�ü�����루λ����̽���Բ�ģ�������λ�õ�ԭ�㣩��  
        ///   
        /// <para>
        /// ������� <seealso cref="Physics.SphereCast"/> �� <seealso cref="Physics.CheckSphere"/>��
        /// </para>  
        /// </summary>  
        /// <param name="maxDistance">̽��Ͷ��������롣</param>  
        /// <param name="density">̽��·���ϲ��Ե���ܶȡ�ֵԽ�ߣ����Ե�Խ�࣬����Խ�ߣ���������ҲԽ��</param>  
        /// <param name="inference">�Ʋⲻ������ײ����Զλ��ʱ�ľ���������ӡ�ֵԽ�ߣ��Ʋ��λ��Լ�ӽ���Զλ�ã��Ʋ�Ľ������ȷ��Խ�ͣ���������ԽС��</param>  
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
        public static DetectionResult Detection(Probe probe, float maxDistance, float density, LayerMask targetLayer) =>
            Detection(probe, maxDistance, density, 0.5f, targetLayer);



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

            for (float currentDistance = singleDistance; currentDistance < maxDistance; currentDistance += singleDistance)
            {
                Vector3 testPosition = IFMath.MoveTo(original, direction, currentDistance);

                if (!Physics.CheckSphere(testPosition, radius, targetLayer)) noCollision = testPosition;
                else break;
            }

            return noCollision;
        }
    }
}
