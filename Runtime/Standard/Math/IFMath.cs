using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Math
{
    public static class IFMath
    {
        /// <summary>
        /// ���� v ��С�����֡�
        /// </summary>
        public static float FloorPart(float v) => v - Mathf.Floor(v);



        public static float Multiply(float original, params float[] values)
        {
            for (int index = 0; index < values.Length; index++) original *= values[index];
            return original;
        }

        public static float Division(float original, params float[] values)
        {
            for (int index = 0; index < values.Length; index++) original /= values[index];
            return original;
        }



        public static Vector2 MoveTo(Vector2 original, Vector2 direction, float distance) => original + direction.normalized * distance;
        public static Vector3 MoveTo(Vector3 original, Vector3 direction, float distance) => original + direction.normalized * distance;



        public static Vector2 Clamp(Vector2 v, Vector2 min, Vector2 max) =>
            new(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y));

        public static Vector3 Clamp(Vector3 v, Vector3 min, Vector3 max) =>
            new(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y), Mathf.Clamp(v.z, min.z, max.z));

        /// <summary>
        /// ǯ�� v ��С����С�� min��
        /// </summary>
        public static float ClampMin(float v, float min) => v < min ? min : v;

        /// <summary>
        /// ǯ�� v ��󲻵ô��� max��
        /// </summary>
        public static float ClampMax(float v, float max) => v > max ? max : v;

        /// <summary>
        /// ǯ�� v ��С����С�� min��
        /// </summary>
        public static int ClampMin(int v, int min) => v < min ? min : v;

        /// <summary>
        /// ǯ�� v ��󲻵ô��� max��
        /// </summary>
        public static int ClampMax(int v, int max) => v > max ? max : v;

        /// <summary>
        /// ǯ�� v ��С����С�� min��
        /// </summary>
        public static Vector2 ClampMin(Vector2 v, Vector2 min) =>
            new(ClampMin(v.x, min.x), ClampMin(v.y, min.y));

        /// <summary>
        /// ǯ�� v ��󲻵ô��� max��
        /// </summary>
        public static Vector2 ClampMax(Vector2 v, Vector2 max) =>
            new(ClampMax(v.x, max.x), ClampMax(v.y, max.y));

        /// <summary>
        /// ǯ�� v ��С����С�� min��
        /// </summary>
        public static Vector3 ClampMin(Vector3 v, Vector3 min) =>
            new(ClampMin(v.x, min.x), ClampMin(v.y, min.y), ClampMin(v.z, min.z));

        /// <summary>
        /// ǯ�� v ��󲻵ô��� max��
        /// </summary>
        public static Vector3 ClampMax(Vector3 v, Vector3 max) =>
            new(ClampMax(v.x, max.x), ClampMax(v.y, max.y), ClampMax(v.z, max.z));

        public static bool InRange(float v, float min, float max) =>
             v > min && v < max;

        public static bool InRange(Vector2 v, Vector2 min, Vector2 max) =>
             InRange(v.x, min.x, max.x) && InRange(v.y, min.y, max.y);

        public static bool InRange(Vector3 v, Vector3 min, Vector3 max) =>
             InRange(v.x, min.x, max.x) && InRange(v.y, min.y, max.y) && InRange(v.z, min.z, max.z);
    }
}
