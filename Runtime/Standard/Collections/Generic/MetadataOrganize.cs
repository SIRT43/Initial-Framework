using InitialFramework.Traverse;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InitialFramework.Collections.Generic
{
    public interface IMetadataOrganizer
    {
        /// <summary>
        /// 注册元数据，值的 <see cref="Guid"/> 应该是唯一的。
        /// </summary>
        bool RegMetadata(Guid guid);
        bool RemoveMetadata(Guid guid);

        bool HasRegMetadata(Guid guid);
    }

    /// <summary>
    /// 本类是对 <see cref="Organize{TValue}"/> 的元数据扩展。
    /// 
    /// <para>
    /// 另请参阅 <seealso cref="IMetadataOrganizer"/>
    /// </para>
    /// </summary>
    [Serializable]
    public class MetadataOrganize<TValue> : Organize<TValue>, ITraversable, IEmptyableCollection where TValue : IOrganized
    {
        /// <summary>
        /// 元数据。
        /// <br>如果您没有特殊要求，请向本列表注册元数据。</br>
        /// </summary>
        [SerializeField] protected List<Guid> metadata = new();

        int IEmptyableCollection.Count => metadata.Count;
        bool IEmptyableCollection.IsEmpty => (this as IEmptyableCollection).Count == 0;


        public virtual bool RegMetadata(Guid guid)
        {
            if (!HasReg(guid)) return false;
            if (HasRegMetadata(guid)) return false;

            metadata.Add(guid);

            return true;
        }

        public virtual bool RemoveMetadata(Guid guid)
        {
            if (!HasRegMetadata(guid)) return false;

            metadata.Remove(guid);

            return true;
        }

        public virtual bool HasRegMetadata(Guid guid) => metadata.Contains(guid);


        public override void Traverse()
        {
            List<Guid> guids = new(metadata);
            foreach (Guid guid in guids) OnTraverse(GetValue(guid));
        }



        public MetadataOrganize(ValidDictionary<Guid, TValue> dictionary) : base(dictionary) { }
        public MetadataOrganize() { }
    }
}
