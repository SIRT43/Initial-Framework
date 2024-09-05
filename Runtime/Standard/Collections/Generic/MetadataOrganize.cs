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
        /// ע��Ԫ���ݣ�ֵ�� <see cref="Guid"/> Ӧ����Ψһ�ġ�
        /// </summary>
        public bool RegMetadata(Guid guid);
        public bool RemoveMetadata(Guid guid);

        public bool HasRegMetadata(Guid guid);
    }

    /// <summary>
    /// �����Ƕ� <see cref="Organize{TValue, TTraverser}"/> ��Ԫ������չ��
    /// 
    /// <para>
    /// ������� <seealso cref="IMetadataOrganizer"/>
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
        /// Ԫ���ݡ�
        /// <br>�����û������Ҫ�������б�ע��Ԫ���ݡ�</br>
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
        /// ͨ��Ԫ���ݱ�������֯��
        /// 
        /// <para>������� <seealso cref="Traverser{TValue, TEnumerable}"/></para>
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
