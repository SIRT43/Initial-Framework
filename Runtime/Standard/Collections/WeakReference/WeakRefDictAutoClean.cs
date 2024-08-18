using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Timers;

namespace FTGAMEStudio.InitialFramework.Collections.WeakReference
{
    /// <summary>
    /// 弱引用字典。
    /// <br>当访问发生，字典会尝试清理无效的键值对。</br>
    /// <br>使用 <see cref="Timer"/> 自动清理。</br>
    /// 
    /// <para>
    /// 另外，弱引用字典的 Count 是不安全的。
    /// <br>如果您需要使用，请先 <see cref="WeakReferenceDictionary{TKey, TValue}.Refresh"/></br>
    /// </para>
    /// </summary>
    public class WeakRefDictAutoClean<TKey, TValue> : WeakReferenceDictionary<TKey, TValue>, IDisposable where TValue : class
    {
        protected Timer cleaner;

        /// <summary>
        /// 使用 <see cref="Timer"/> 自动清理。
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
