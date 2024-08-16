using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Replicator
{
    /// <summary>
    /// 组件前置复制器，它要求源对象的根 <see cref="GameObject"/> 上必须包含指定的组件。
    /// 
    /// <para>另请参阅 <see cref="GameObjectReplicator"/></para>
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
