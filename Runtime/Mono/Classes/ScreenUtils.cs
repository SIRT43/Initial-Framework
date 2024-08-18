using UnityEngine;

namespace FTGAMEStudio.InitialFramework
{
    /// <summary>
    /// �����װ�˹�����Ļ����ķ�����
    /// </summary>
    public static class ScreenUtils
    {
        /// <summary>
        /// ʵ���˶� <see cref="RectTransformUtility.ScreenPointToLocalPointInRectangle"/> �ķ�װ��
        /// 
        /// <para>
        /// ������� <seealso cref="ScreenToLocalPoi(Vector2, RectTransform, out Vector2, Camera)"/>
        /// </para>
        /// </summary>
        public static Vector2 ScreenToLocalPoi(Vector2 screenPoint, RectTransform target, Camera camera = null)
        {
            bool result = ScreenToLocalPoi(screenPoint, target, out Vector2 localPoi, camera);
            return result ? localPoi : new(Mathf.Infinity, Mathf.Infinity);
        }
        /// <summary>
        /// ʵ���˶� <see cref="RectTransformUtility.ScreenPointToLocalPointInRectangle"/> �ķ�װ��
        /// </summary>
        public static bool ScreenToLocalPoi(Vector2 screenPoint, RectTransform target, out Vector2 localPoint, Camera camera = null)
        {
            bool result = RectTransformUtility.ScreenPointToLocalPointInRectangle(target, screenPoint, camera, out Vector2 localPoi);
            localPoint = localPoi;

            return result;
        }
        /// <summary>
        /// ʵ���˶� <see cref="RectTransformUtility.ScreenPointToWorldPointInRectangle"/> �ķ�װ��
        /// 
        /// <para>
        /// ������� <seealso cref="ScreenToWorldPoi(Vector2, RectTransform, out Vector3, Camera)"/>
        /// </para>
        /// </summary>
        public static Vector3 ScreenToWorldPoi(Vector2 screenPoint, RectTransform target, Camera camera = null)
        {
            bool result = ScreenToWorldPoi(screenPoint, target, out Vector3 worldPoi, camera);
            return result ? worldPoi : new(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
        }
        /// <summary>
        /// ʵ���˶� <see cref="RectTransformUtility.ScreenPointToWorldPointInRectangle"/> �ķ�װ��
        /// </summary>
        public static bool ScreenToWorldPoi(Vector2 screenPoint, RectTransform target, out Vector3 worldPoint, Camera camera = null)
        {
            bool result = RectTransformUtility.ScreenPointToWorldPointInRectangle(target, screenPoint, camera, out Vector3 worldPoi);
            worldPoint = worldPoi;

            return result;
        }
    }
}
