using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Timers;

namespace FTGAMEStudio.InitialFramework.Collections.WeakReference
{
    /// <summary>
    /// 弱引用自动清理字典，派生自 <see cref="WeakReferenceDictionary{TKey, TValue}"/>。
    /// 
    /// <para>弱引用自动清理字典会在无操作时自动清理无效键值对 (指 <see cref="WeakReference.Target"/> 为 null 或键值为 null。)，且访问发生时如果键值对无效则弱引用字典会尝试移除它们。
    /// <br>请不要尝试序列化本字典或派生自本字典的对象，这可能引入不必要的错误。</br></para>
    /// </summary>
    public class WeakRefDictAutoClean<TKey, TValue> : WeakReferenceDictionary<TKey, TValue>, IDisposable where TValue : class
    {
        protected Timer cleaner;

        /// <summary>
        /// 本字典使用 <see cref="Timer"/> 进行定时清理。
        /// <br>有关 <see cref="Timer"/>，详见 https://learn.microsoft.com/zh-cn/dotnet/api/system.timers.timer?view=netstandard-2.1</br>
        /// </summary>
        public WeakRefDictAutoClean(double interval) : base()
        {
            cleaner = new()
            {
                Interval = interval,
                AutoReset = true,
            };

            cleaner.Elapsed += Cleaner_Elapsed;
            cleaner.Start();
        }

        protected virtual void Cleaner_Elapsed(object sender, ElapsedEventArgs e) => Refresh();

        /// <summary>
        /// 释放 <see cref="Timer"/> 的托管资源。
        /// <br>本操作不可逆，请确保在此之后您不会继续使用本实例。</br>
        /// </summary>
        public virtual void Dispose()
        {
            cleaner.Stop();
            cleaner.Close();

            GC.SuppressFinalize(this);
        }

        ~WeakRefDictAutoClean() => Dispose();

        public WeakRefDictAutoClean() : this(60000) { }
        public WeakRefDictAutoClean(IDictionary<TKey, WeakReference<TValue>> dictionary) : base(dictionary) { }
        public WeakRefDictAutoClean(IDictionary<TKey, WeakReference<TValue>> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }
        public WeakRefDictAutoClean(IEnumerable<KeyValuePair<TKey, WeakReference<TValue>>> collection) : base(collection) { }
        public WeakRefDictAutoClean(IEnumerable<KeyValuePair<TKey, WeakReference<TValue>>> collection, IEqualityComparer<TKey> comparer) : base(collection, comparer) { }
        public WeakRefDictAutoClean(IEqualityComparer<TKey> comparer) : base(comparer) { }
        public WeakRefDictAutoClean(int capacity) : base(capacity) { }
        public WeakRefDictAutoClean(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
        protected WeakRefDictAutoClean(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
