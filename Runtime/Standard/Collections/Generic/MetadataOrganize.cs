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
        ITraversable<TTraverser, Dictionary<Guid, TValue>.ValueCollection, TValue>,
        IEmptyableCollection
        where TValue : IOrganized
        where TTraverser : Traverser<Dictionary<Guid, TValue>.ValueCollection, TValue>, new()
    {
        /// <summary>
        /// Ԫ���ݡ�
        /// <br>�����û������Ҫ�������б�ע��Ԫ���ݡ�</br>
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
        /// ͨ��Ԫ���ݱ�������֯��
        /// 
        /// <para>������� <seealso cref="Traverser{TValue, TEnumerable}"/></para>
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
