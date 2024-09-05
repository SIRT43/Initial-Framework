using UnityEngine;

namespace FTGAMEStudio.InitialFramework
{
    public readonly struct DetectionResult
    {
        /// <summary>
        /// 探针最后一次未发生碰撞的圆心点。
        /// </summary>
        public readonly Vector3 position;
        /// <summary>
        /// 原点到圆心点的距离。
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
}
