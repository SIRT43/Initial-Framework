using UnityEngine;

namespace InitialFramework
{
    public interface IDestructionHandler
    {
        void OnDestructionArea();
    }

    [AddComponentMenu("Initial Framework/Destruction Area"), RequireComponent(typeof(Collider)), ExecuteInEditMode]
    public class DestructionArea : MonoBehaviour
    {
        protected virtual void Awake()
        {
            foreach (Collider collider in GetComponents<Collider>()) 
                if (!collider.isTrigger) 
                    Debug.LogWarning($"Collider '{collider.name}' is not set as a trigger. This may prevent OnTriggerEnter from being called as expected.", collider);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            GameObject destroyTarget = other.attachedRigidbody != null ? other.attachedRigidbody.gameObject : other.gameObject;

            foreach (MonoBehaviour behaviour in destroyTarget.GetComponents<MonoBehaviour>()) 
                if (behaviour is IDestructionHandler handler) 
                    handler.OnDestructionArea();

            Destroy(destroyTarget);
        }
    }
}
