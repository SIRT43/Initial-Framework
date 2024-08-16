using System;

namespace FTGAMEStudio.InitialFramework.ExtensionMethods
{
    /// <summary>
    /// 对 <see cref="WeakReference"/> 的扩展方法。
    /// </summary>
    public static class WeakReferenceMethods
    {
        /// <summary>
        /// 直接获取弱引用目标，结果可能为 null。
        /// </summary>
        public static T GetTarget<T>(this WeakReference<T> weakReference) where T : class
        {
            if (!weakReference.TryGetTarget(out T target)) return null;
            return target;
        }
    }
}
