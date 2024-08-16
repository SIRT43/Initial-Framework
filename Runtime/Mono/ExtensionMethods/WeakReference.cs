using System;

namespace FTGAMEStudio.InitialFramework.ExtensionMethods
{
    /// <summary>
    /// �� <see cref="WeakReference"/> ����չ������
    /// </summary>
    public static class WeakReferenceMethods
    {
        /// <summary>
        /// ֱ�ӻ�ȡ������Ŀ�꣬�������Ϊ null��
        /// </summary>
        public static T GetTarget<T>(this WeakReference<T> weakReference) where T : class
        {
            if (!weakReference.TryGetTarget(out T target)) return null;
            return target;
        }
    }
}
