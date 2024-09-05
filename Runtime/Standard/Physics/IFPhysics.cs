using UnityEngine;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>
    /// 本类基于 <see cref="Physics"/> 封装提供了更多方法。
    /// </summary>
    public static class IFPhysics
    {
        /// <summary>  
        /// 向指定方向投射探针，探测不发生碰撞的最远位置及其距离（位置是探针的圆心，距离是位置到原点）。  
        ///   
        /// <para>
        /// 另请参阅 <seealso cref="Physics.SphereCast"/> 和 <seealso cref="Physics.CheckSphere"/>。
        /// </para>  
        /// </summary>  
        /// <param name="maxDistance">探针投射的最大距离。</param>  
        /// <param name="density">探测路径上测试点的密度。值越高，测试点越多，精度越高，但计算量也越大。</param>  
        /// <param name="inference">推测不发生碰撞的最远位置时的距离调整因子。值越高，推测的位置约接近最远位置，推测的结果的正确率越低，但计算量越小。</param>  
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
        /// 向指定方向投射探针，探测不发生碰撞的最远位置及其距离（位置是探针的圆心，距离是位置到原点）。  
        ///   
        /// <para>
        /// 另请参阅 <seealso cref="Physics.SphereCast"/> 和 <seealso cref="Physics.CheckSphere"/>。
        /// </para>  
        /// </summary>  
        /// <param name="maxDistance">探针投射的最大距离。</param>  
        /// <param name="density">探测路径上测试点的密度。值越高，测试点越多，精度越高，但计算量也越大。</param>  
        /// <param name="inference">推测不发生碰撞的最远位置时的距离调整因子。值越高，推测的位置约接近最远位置，推测的结果的正确率越低，但计算量越小。</param>  
        public static DetectionResult Detection(Probe probe, float maxDistance, float density, float inference) =>
            Detection(probe, maxDistance, density, inference, ~4);

        /// <summary>  
        /// 向指定方向投射探针，探测不发生碰撞的最远位置及其距离（位置是探针的圆心，距离是位置到原点）。  
        ///   
        /// <para>
        /// 另请参阅 <seealso cref="Physics.SphereCast"/> 和 <seealso cref="Physics.CheckSphere"/>。
        /// </para>  
        /// </summary>  
        /// <param name="maxDistance">探针投射的最大距离。</param>  
        /// <param name="density">探测路径上测试点的密度。值越高，测试点越多，精度越高，但计算量也越大。</param>  
        public static DetectionResult Detection(Probe probe, float maxDistance, float density) =>
            Detection(probe, maxDistance, density, 0.5f);

        /// <summary>  
        /// 向指定方向投射探针，探测不发生碰撞的最远位置及其距离（位置是探针的圆心，距离是位置到原点）。  
        ///   
        /// <para>
        /// 另请参阅 <seealso cref="Physics.SphereCast"/> 和 <seealso cref="Physics.CheckSphere"/>。
        /// </para>  
        /// </summary>  
        /// <param name="maxDistance">探针投射的最大距离。</param>  
        public static DetectionResult Detection(Probe probe, float maxDistance) =>
            Detection(probe, maxDistance, 1);

        /// <summary>  
        /// 向指定方向投射探针，探测不发生碰撞的最远位置及其距离（位置是探针的圆心，距离是位置到原点）。  
        ///   
        /// <para>
        /// 另请参阅 <seealso cref="Physics.SphereCast"/> 和 <seealso cref="Physics.CheckSphere"/>。
        /// </para>  
        /// </summary>  
        /// <param name="maxDistance">探针投射的最大距离。</param>  
        /// <param name="density">探测路径上测试点的密度。值越高，测试点越多，精度越高，但计算量也越大。</param>  
        public static DetectionResult Detection(Probe probe, float maxDistance, float density, LayerMask targetLayer) =>
            Detection(probe, maxDistance, density, 0.5f, targetLayer);



        /// <summary>
        /// 尝试向指定方向查找指定距离与半径的最大可达点。
        /// <br>不推荐使用此方法，请使用 <see cref="Detection"/>。</br>
        /// </summary>
        /// <param name="original">查找原点。</param>
        /// <param name="direction">查找方向。</param>
        /// <param name="radius">查找体半径。</param>
        /// <param name="singleDistance">每查找一次增加的距离。</param>
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
