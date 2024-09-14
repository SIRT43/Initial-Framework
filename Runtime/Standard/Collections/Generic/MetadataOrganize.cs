using InitialFramework.Traverse;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InitialFramework.Collections.Generic
{
    public interface IMetadataOrganizer
    {
        /// <summary>
        /// ע��Ԫ���ݣ�ֵ�� <see cref="Guid"/> Ӧ����Ψһ�ġ�
        /// </summary>
        bool RegMetadata(Guid guid);
        bool RemoveMetadata(Guid guid);

        bool HasRegMetadata(Guid guid);
    }

    /// <summary>
    /// �����Ƕ� <see cref="Organize{TValue}"/> ��Ԫ������չ��
    /// 
    /// <para>
    /// ������� <seealso cref="IMetadataOrganizer"/>
    /// </para>
    /// </summary>
    [Serializable]
    public class MetadataOrganize<TValue> : Organize<TValue>, ITraversable, IEmptyableCollection where TValue : IOrganized
    {
        /// <summary>
        /// Ԫ���ݡ�
        /// <br>�����û������Ҫ�������б�ע��Ԫ���ݡ�</br>
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
