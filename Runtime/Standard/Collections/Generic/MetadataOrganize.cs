using FTGAMEStudio.InitialFramework.Traverse;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Collections.Generic
{
    public interface IMetadataOrganizer
    {
        event Action<int, bool> OnMetadataCountChanged;

        int MetadataCount { get; }
        bool IsMetadataEmpty { get; }

        /// <summary>
        /// ע��Ԫ���ݣ�ֵ�� <see cref="Guid"/> Ӧ����Ψһ�ġ�
        /// </summary>
        bool RegMetadata(Guid guid);
        bool RemoveMetadata(Guid guid);

        bool HasRegMetadata(Guid guid);
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
        ITraversable<TTraverser, Dictionary<Guid, TValue>, KeyValuePair<Guid, TValue>>
        where TValue : IOrganized
        where TTraverser : Traverser<Dictionary<Guid, TValue>, KeyValuePair<Guid, TValue>>, new()
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
            if (!HasRegMetadata(guid)) return false;

            metadata.Remove(guid);
            OnMetadataCountChanged?.Invoke(MetadataCount, IsMetadataEmpty);

            return true;
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



        public MetadataOrganize(ValidDictionary<Guid, TValue> dictionary, TTraverser traverser) : base(dictionary, traverser) { }
        public MetadataOrganize(ValidDictionary<Guid, TValue> dictionary) : base(dictionary) { }
        public MetadataOrganize(TTraverser traverser) : base(traverser) { }
        public MetadataOrganize() { }
    }
}
