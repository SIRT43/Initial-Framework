using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Timers;

namespace FTGAMEStudio.InitialFramework.Collections.WeakReference
{
    /// <summary>
    /// �������Զ������ֵ䣬������ <see cref="WeakReferenceDictionary{TKey, TValue}"/>��
    /// 
    /// <para>�������Զ������ֵ�����޲���ʱ�Զ�������Ч��ֵ�� (ָ <see cref="WeakReference.Target"/> Ϊ null ���ֵΪ null��)���ҷ��ʷ���ʱ�����ֵ����Ч���������ֵ�᳢���Ƴ����ǡ�
    /// <br>�벻Ҫ�������л����ֵ�������Ա��ֵ�Ķ�����������벻��Ҫ�Ĵ���</br></para>
    /// </summary>
    public class WeakRefDictAutoClean<TKey, TValue> : WeakReferenceDictionary<TKey, TValue>, IDisposable where TValue : class
    {
        protected Timer cleaner;

        /// <summary>
        /// ���ֵ�ʹ�� <see cref="Timer"/> ���ж�ʱ����
        /// <br>�й� <see cref="Timer"/>����� https://learn.microsoft.com/zh-cn/dotnet/api/system.timers.timer?view=netstandard-2.1</br>
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
