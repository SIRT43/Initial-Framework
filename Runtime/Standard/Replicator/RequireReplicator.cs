using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Replicator
{
    /// <summary>
    /// ���ǰ�ø���������Ҫ��Դ����ĸ� <see cref="GameObject"/> �ϱ������ָ���������
    /// 
    /// <para>������� <see cref="GameObjectReplicator"/></para>
    /// </summary>
    public abstract class RequireReplicator<T> : GameObjectReplicator where T : Component
    {
        public override GameObject Original
        {
            get => base.Original;
            set
            {
                if (value == null) return;
                if (value.GetComponent<T>() == null)
                {
                    Debug.LogWarning($"Cannot set GameObject: {value.name}, the replicator requires it to contain the specified component: {typeof(T).FullName}");
                    return;
                }

                original = value;
            }
        }

        public virtual T SingleWithComponent() => SingleObject().GetComponent<T>();

        public virtual T[] MultipleWithComponent(int count)
        {
            T[] components = new T[count];

            for (int index = 0; index < count; index++) components[index] = SingleWithComponent();

            return components;
        }
    }
}
