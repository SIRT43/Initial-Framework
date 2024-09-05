using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Replicator
{
    public interface IGameObjectReplicator
    {
        public GameObject SingleObject();
        public GameObject[] MultipleObject(int count);
    }

    [AddComponentMenu("Initial Framework/Replicator/GameObject Replicator")]
    public class GameObjectReplicator : MonoBehaviour, IGameObjectReplicator
    {
        protected GameObject original;
        protected Transform parent;

        protected bool worldPositionStays = false;
        protected Transform align;

        protected bool overrideName = false;
        protected string newObjectName;


        private int replicateCount = 0;


        public virtual GameObject Original { get => original; set => original = value == null ? original : value; }
        public virtual Transform Parent { get => parent; set => parent = value; }


        public virtual bool WorldPositionStays
        {
            get => worldPositionStays;
            set
            {
                if (value && Align == null) Align = transform;
                worldPositionStays = value;
            }
        }

        public virtual Transform Align
        {
            get => align;
            set
            {
                if (value == null && WorldPositionStays) return;
                align = value;
            }
        }


        public virtual bool OverrideName { get => overrideName; set => overrideName = value; }
        public virtual string NewObjectName { get => newObjectName; set => newObjectName = value; }


        public virtual int ReplicateCount { get => replicateCount; }


        public virtual GameObject SingleObject()
        {
            replicateCount++;

            GameObject newObject = Instantiate(original, parent);
            newObject.name = $"{(overrideName ? newObjectName : original.name)}_{replicateCount}";

            if (worldPositionStays) newObject.transform.SetPositionAndRotation(align.position, align.rotation);

            newObject.transform.SetParent(parent, worldPositionStays);

            return newObject;
        }

        public virtual GameObject[] MultipleObject(int count)
        {
            GameObject[] newObjects = new GameObject[count];

            for (int index = 0; index < count; index++) newObjects[index] = SingleObject();

            return newObjects;
        }
    }
}
