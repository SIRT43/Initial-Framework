using UnityEngine;

namespace InitialFramework
{
    /// <summary>
    /// ���� <see cref="Random"/> ����չ�ࡣ
    /// </summary>
    public static class IFRandom
    {
        /// <summary>
        /// ����������
        /// </summary>
        public static Vector2 Range(Vector2 min, Vector2 max) => new(
             Random.Range(min.x, max.x),
             Random.Range(min.y, max.y));

        /// <summary>
        /// ����������
        /// </summary>
        public static Vector3 Range(Vector3 min, Vector3 max) => new(
             Random.Range(min.x, max.x),
             Random.Range(min.y, max.y),
             Random.Range(min.z, max.z));
    }
}
