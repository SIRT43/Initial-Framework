using System;
using System.Collections.Generic;

namespace InitialFramework.Classifying
{
    public interface IMonoFilterable<TKey, TValue>
    {
        event Func<TValue, Dictionary<TKey, TValue>, TKey> KeyGenerator;
        event Func<TValue, Dictionary<TKey, TValue>, bool> ValueFilter;
    }

    public interface IMultiFilterable<TKey, TValue>
    {
        event Func<TValue, Dictionary<TKey, List<TValue>>, TKey> KeyGenerator;
        event Func<TValue, Dictionary<TKey, List<TValue>>, bool> ValueFilter;
    }
}
