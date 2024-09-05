using FTGAMEStudio.InitialFramework.Traverse;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Collections.Generic
{
    public interface IMetadataOrganizer
    {
        public event Action<int, bool> OnMetadataCountChanged;

        public int MetadataCount { get; }
        public bool IsMetadataEmpty { get; }

        /// <summary>
        /// 注册元数据，值的 <see cref="Guid"/> 应该是唯一的。
        /// </summary>
        public bool RegMetadata(Guid guid);
        public bool RemoveMetadata(Guid guid);

        public bool HasRegMetadata(Guid guid);
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
        ITraversable<KeyValuePair<Guid, TValue>, Dictionary<Guid, TValue>, TTraverser>
        where TValue : IOrganized
        where TTraverser : Traverser<KeyValuePair<Guid, TValue>, Dictionary<Guid, TValue>>
    {
        /// <summary>
        /// 元数据。
        /// <br>如果您没有特殊要求，请向本列表注册元数据。</br>
        /// </summary>
        [SerializeField] protected Dictionary<Guid, TValue> metadata = new();

        public int MetadataCount => metadata.Count;
        public bool IsMetadataEmpty => MetadataCount == 0;

        public event Action<int, bool> OnMetadataCountChanged;

        public virtual bool RegMetadata(Guid guid)
        {
            if (!HasReg(guid)) return false;
            if (HasRegMetadata(guid)) return false;

            metadata.Add(guid, GetValue(guid));
            OnMetadataCountChanged?.Invoke(MetadataCount, IsMetadataEmpty);

            return true;
        }

        public virtual bool RemoveMetadata(Guid guid)
        {
            bool result = metadata.Remove(guid);
            OnMetadataCountChanged?.Invoke(MetadataCount, IsMetadataEmpty);

            return result;
        }

        public virtual bool HasRegMetadata(Guid guid) => metadata.ContainsKey(guid);

        /// <summary>
        /// 通过元数据遍历此组织。
        /// 
        /// <para>另请参阅 <seealso cref="Traverser{TValue, TEnumerable}"/></para>
        /// </summary>
        public override void Traverse() => Traverser.Traverse(metadata);

        public override bool RemoveValue(Guid guid)
        {
            metadata.Remove(guid);
            return base.RemoveValue(guid);
        }


        protected MetadataOrganize(TTraverser traverser) : base(traverser) { }
        protected MetadataOrganize(SecurityDictionary<Guid, TValue> dictionary, TTraverser traverser) : base(dictionary, traverser) { }
    }
}
