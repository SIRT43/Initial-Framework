using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Collections.Liner
{
    [Serializable]
    public abstract class LinerCollectionBase<T> : ILinerCollection<T>, IEmptyableCollection, IClearable, IEnumerable<T>
    {
        [SerializeField] private readonly List<T> values;

        public int Count => values.Count;
        public bool IsEmpty => Count == 0;

        public abstract int OperateIndex { get; }


        protected LinerCollectionBase(List<T> values) => this.values = values;
        protected LinerCollectionBase() : this(new()) { }


        public virtual void Push(T value) => values.Add(value);


        public virtual T Pop()
        {
            if (!TryPop(out T result))
                throw new InvalidOperationException("Values is empty.");

            return result;
        }

        public virtual bool TryPop(out T result)
        {
            if (!IsEmpty)
            {
                result = values[OperateIndex];
                values.RemoveAt(OperateIndex);

                return true;
            }

            result = default;
            return false;
        }


        public virtual T Peek()
        {
            if (!TryPeek(out T result))
                throw new InvalidOperationException("Values is empty.");

            return result;
        }

        public virtual bool TryPeek(out T result)
        {
            if (!IsEmpty)
            {
                result = values[OperateIndex];
                return true;
            }

            result = default;
            return false;
        }


        public virtual void Clear() => values.Clear();


        public virtual IEnumerator<T> GetEnumerator() => values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => values.GetEnumerator();
    }
}
