using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Math
{
    public static class IFMath
    {
        /// <summary>
        /// 返回 v 的小数部分。
        /// </summary>
        public static float FloorPart(float v) => v - Mathf.Floor(v);


        /// <summary>
        /// 逐个相乘。
        /// </summary>
        public static float Multiply(float original, params float[] values)
        {
            for (int index = 0; index < values.Length; index++) original *= values[index];
            return original;
        }

        /// <summary>
        /// 逐个相除
        /// </summary>
        public static float Division(float original, params float[] values)
        {
            for (int index = 0; index < values.Length; index++) original /= values[index];
            return original;
        }


        /// <summary>
        /// 将指定点向指定方向移动指定距离。
        /// </summary>
        public static Vector2 MoveTo(Vector2 original, Vector2 direction, float distance) => original + direction.normalized * distance;
        /// <summary>
        /// 将指定点向指定方向移动指定距离。
        /// </summary>
        public static Vector3 MoveTo(Vector3 original, Vector3 direction, float distance) => original + direction.normalized * distance;


        /// <summary>
        /// 逐分量钳制。
        /// </summary>
        public static Vector2 Clamp(Vector2 v, Vector2 min, Vector2 max) =>
            new(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y));

        /// <summary>
        /// 逐分量钳制。
        /// </summary>
        public static Vector3 Clamp(Vector3 v, Vector3 min, Vector3 max) =>
            new(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y), Mathf.Clamp(v.z, min.z, max.z));


        /// <summary>
        /// 钳制 v 最小不得小于 min。
        /// </summary>
        public static float ClampMin(float v, float min) => v < min ? min : v;

        /// <summary>
        /// 钳制 v 最大不得大于 max。
        /// </summary>
        public static float ClampMax(float v, float max) => v > max ? max : v;

        /// <summary>
        /// 钳制 v 最小不得小于 min。
        /// </summary>
        public static int ClampMin(int v, int min) => v < min ? min : v;

        /// <summary>
        /// 钳制 v 最大不得大于 max。
        /// </summary>
        public static int ClampMax(int v, int max) => v > max ? max : v;


        /// <summary>
        /// 逐分量钳制 v 最小不得小于 min。
        /// </summary>
        public static Vector2 ClampMin(Vector2 v, Vector2 min) =>
            new(ClampMin(v.x, min.x), ClampMin(v.y, min.y));

        /// <summary>
        /// 逐分量钳制 v 最大不得大于 max。
        /// </summary>
        public static Vector2 ClampMax(Vector2 v, Vector2 max) =>
            new(ClampMax(v.x, max.x), ClampMax(v.y, max.y));

        /// <summary>
        /// 逐分量钳制 v 最小不得小于 min。
        /// </summary>
        public static Vector3 ClampMin(Vector3 v, Vector3 min) =>
            new(ClampMin(v.x, min.x), ClampMin(v.y, min.y), ClampMin(v.z, min.z));

        /// <summary>
        /// 逐分量钳制 v 最大不得大于 max。
        /// </summary>
        public static Vector3 ClampMax(Vector3 v, Vector3 max) =>
            new(ClampMax(v.x, max.x), ClampMax(v.y, max.y), ClampMax(v.z, max.z));


        /// <summary>
        /// 判断值是否在指定值之间。
        /// </summary>
        public static bool InRange(float v, float min, float max) =>
             v > min && v < max;

        /// <summary>
        /// 逐分量判断值是否在指定值之间。
        /// </summary>
        public static bool InRange(Vector2 v, Vector2 min, Vector2 max) =>
             InRange(v.x, min.x, max.x) && InRange(v.y, min.y, max.y);

        /// <summary>
        /// 逐分量判断值是否在指定值之间。
        /// </summary>
        public static bool InRange(Vector3 v, Vector3 min, Vector3 max) =>
             InRange(v.x, min.x, max.x) && InRange(v.y, min.y, max.y) && InRange(v.z, min.z, max.z);


        /// <summary>
        /// 是否是负数。
        /// </summary>
        public static bool IsNegative(float v) => v < 0;
        public static bool IsNegative(int v) => v < 0;
    }
}
