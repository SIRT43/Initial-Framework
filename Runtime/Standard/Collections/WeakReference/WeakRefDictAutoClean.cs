using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Timers;

namespace FTGAMEStudio.InitialFramework.Collections.WeakReference
{
    /// <summary>
    /// �������ֵ䡣
    /// <br>�����ʷ������ֵ�᳢��������Ч�ļ�ֵ�ԡ�</br>
    /// <br>ʹ�� <see cref="Timer"/> �Զ�����</br>
    /// 
    /// <para>
    /// ���⣬�������ֵ�� Count �ǲ���ȫ�ġ�
    /// <br>�������Ҫʹ�ã����� <see cref="WeakReferenceDictionary{TKey, TValue}.Refresh"/></br>
    /// </para>
    /// </summary>
    public class WeakRefDictAutoClean<TKey, TValue> : WeakReferenceDictionary<TKey, TValue>, IDisposable where TValue : class
    {
        protected Timer cleaner;

        /// <summary>
        /// ʹ�� <see cref="Timer"/> �Զ�����
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
        /// �ͷ� <see cref="Timer"/> ���й���Դ��
        /// <br>�����������棬��ȷ���ڴ�֮�����������ʹ�ñ�ʵ����</br>
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
