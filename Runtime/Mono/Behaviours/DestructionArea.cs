using UnityEngine;

namespace FTGAMEStudio.InitialFramework
{
    public interface IDestructionHandler
    {
        void OnDestructionArea();
    }

    [AddComponentMenu("Initial Framework/Destruction Area"), RequireComponent(typeof(Collider)), ExecuteInEditMode]
    public class DestructionArea : MonoBehaviour
    {
        public Collider[] colliders;

        protected virtual void Awake()
        {
            colliders = GetComponents<Collider>();
            foreach (Collider collider in colliders) collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject destroyTarget = other.attachedRigidbody == null ? other.gameObject : other.attachedRigidbody.gameObject;
            MonoBehaviour[] component = destroyTarget.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour behaviour in component) if (behaviour is IDestructionHandler handler) handler.OnDestructionArea();
            Destroy(destroyTarget);
        }
    }
}
