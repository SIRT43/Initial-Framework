using InitialFramework.Traverse;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InitialFramework.Collections.Generic
{
    public interface IMetadataOrganizer
    {
        event Action<int, bool> OnMetadataCountChanged;

        /// <summary>
        /// 注册元数据，值的 <see cref="Guid"/> 应该是唯一的。
        /// </summary>
        bool RegMetadata(Guid guid);
        bool RemoveMetadata(Guid guid);

        bool HasRegMetadata(Guid guid);
    }

    /// <summary>
    /// 本类是对 <see cref="Organize{TValue, TTraverser}"/> 的元数据扩展。
    /// 
    /// <para>
    /// 另请参阅 <seealso cref="IMetadataOrganizer"/>
    /// </para>
    /// </summary>
    [Serializable]
    public class MetadataOrganize<TValue, TTraverser> :
        Organize<TValue, TTraverser>,
        ITraversable<TTraverser, Dictionary<Guid, TValue>.ValueCollection, TValue>,
        IEmptyableCollection
        where TValue : IOrganized
        where TTraverser : Traverser<Dictionary<Guid, TValue>.ValueCollection, TValue>, new()
    {
        /// <summary>
        /// 元数据。
        /// <br>如果您没有特殊要求，请向本列表注册元数据。</br>
        /// </summary>
        [SerializeField] protected Dictionary<Guid, TValue> metadata = new();

        int IEmptyableCollection.Count => metadata.Count;
        bool IEmptyableCollection.IsEmpty => (this as IEmptyableCollection).Count == 0;

        public event Action<int, bool> OnMetadataCountChanged;

        public virtual bool RegMetadata(Guid guid)
        {
            if (!HasReg(guid)) return false;
            if (HasRegMetadata(guid)) return false;

            metadata.Add(guid, GetValue(guid));
            OnMetadataCountChanged?.Invoke(metadata.Count, metadata.Count == 0);

            return true;
        }

        public virtual bool RemoveMetadata(Guid guid)
        {
            if (!HasRegMetadata(guid)) return false;

            metadata.Remove(guid);
            OnMetadataCountChanged?.Invoke(metadata.Count, metadata.Count == 0);

            return true;
        }

        public virtual bool HasRegMetadata(Guid guid) => metadata.ContainsKey(guid);

        /// <summary>
        /// 通过元数据遍历此组织。
        /// 
        /// <para>另请参阅 <seealso cref="Traverser{TValue, TEnumerable}"/></para>
        /// </summary> 
        public override void Traverse() => Traverser.Traverse(metadata.Values);

        public override bool RemoveValue(Guid guid)
        {
            metadata.Remove(guid);
            return base.RemoveValue(guid);
        }



        public MetadataOrganize(ValidDictionary<Guid, TValue> dictionary, TTraverser traverser) : base(dictionary, traverser) { }
        public MetadataOrganize(ValidDictionary<Guid, TValue> dictionary) : base(dictionary) { }
        public MetadataOrganize(TTraverser traverser) : base(traverser) { }
        public MetadataOrganize() { }
    }
}
