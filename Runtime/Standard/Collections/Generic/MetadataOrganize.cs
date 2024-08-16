using System;
using System.Collections.Generic;

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

        public void TraverseMetadata();
    }

    /// <summary>
    /// �����Ƕ� <see cref="Organize{TValue}"/> ��Ԫ������չ��
    /// 
    /// <para>
    /// ������� <seealso cref="IMetadataOrganizer"/>
    /// </para>
    /// </summary>
    public abstract class MetadataOrganize<TValue> : Organize<TValue>, IMetadataOrganizer where TValue : IOrganized
    {
        /// <summary>
        /// Ԫ���ݡ�
        /// <br>�����û������Ҫ�������б�ע��Ԫ���ݡ�</br>
        /// </summary>
        protected List<Guid> metadata = new();

        public int MetadataCount => metadata.Count;
        public bool IsMetadataEmpty => MetadataCount == 0;

        public event Action<int, bool> OnMetadataCountChanged;

        public virtual bool RegMetadata(Guid guid)
        {
            if (HasRegMetadata(guid)) return false;
            if (!HasReg(guid)) return false;

            metadata.Add(guid);
            OnMetadataCountChanged?.Invoke(MetadataCount, IsMetadataEmpty);

            return true;
        }

        public virtual bool RemoveMetadata(Guid guid)
        {
            bool result = metadata.Remove(guid);
            OnMetadataCountChanged?.Invoke(MetadataCount, IsMetadataEmpty);

            return result;
        }

        public virtual bool HasRegMetadata(Guid guid) => metadata.Contains(guid);

        /// <summary>
        /// ͨ��Ԫ���ݱ�������֯��
        /// 
        /// <para>������� <seealso cref="OnTraverseMetadata(KeyValuePair{Guid, TExternalValue})"/></para>
        /// </summary>
        public virtual void TraverseMetadata()
        {
            Guid[] guids = metadata.ToArray();
            foreach (Guid guid in guids) OnTraverseMetadata(new(guid, GetValue(guid)));
        }

        /// <summary>
        /// ����ͨ��Ԫ���ݱ���ʱ���ô˷�����
        /// 
        /// <para>������� <seealso cref="TraverseMetadata"/></para>
        /// </summary>
        protected virtual void OnTraverseMetadata(KeyValuePair<Guid, TValue> pair) { }

        public override bool RemoveValue(Guid guid)
        {
            RemoveMetadata(guid);
            return base.RemoveValue(guid);
        }

        protected MetadataOrganize() : base() { }
        protected MetadataOrganize(SecurityDictionary<Guid, TValue> dictionary) : base(dictionary) { }
    }
}
