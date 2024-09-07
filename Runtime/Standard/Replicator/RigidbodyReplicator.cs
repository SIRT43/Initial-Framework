using UnityEngine;

namespace FTGAMEStudio.InitialFramework.Replicator
{
    [AddComponentMenu("Initial Framework/Replicator/Rigidbody Replicator")]
    public class RigidbodyReplicator : RequireReplicator<Rigidbody>
    {
        //Ϊ Rigidbody ��ӳ�ʼ�����̶��������
        public bool applyInitialForce = false;
        public RandomMode forceRandomMode = RandomMode.Fixed;

        public Vector3 fixedForce;
        public Vector3Random randomForce;


        //Ϊ Rigidbody ��ӳ�ʼ�ٶȣ��̶��������
        public bool applyInitialVelocity = false;
        public RandomMode velocityRandomMode = RandomMode.Fixed;

        public Vector3 fixedVelocity;
        public Vector3Random randomVelocity;


        //Ϊ Rigidbody ��ӳ�ʼ���ٶȣ��̶��������
        public bool applyInitialAngularVelocity = false;
        public RandomMode angularVelocityRandomMode = RandomMode.Fixed;

        public Vector3 fixedAngularVelocity;
        public Vector3Random randomAngularVelocity;


        public override GameObject SingleObject()
        {
            GameObject gameObject = base.SingleObject();

            if (!gameObject.TryGetComponent(out Rigidbody rigidbody)) return gameObject;

            if (applyInitialForce)
            {
                switch (forceRandomMode)
                {
                    case RandomMode.Fixed:
                    default:
                        rigidbody.AddForce(fixedForce);
                        break;
                    case RandomMode.Random:
                        rigidbody.AddForce(randomForce.Range());
                        break;
                }
            }

            if (applyInitialVelocity) rigidbody.velocity = velocityRandomMode switch
            {
                RandomMode.Fixed => fixedVelocity,
                RandomMode.Random => randomVelocity.Range(),
                _ => fixedVelocity
            };

            if (applyInitialAngularVelocity) rigidbody.angularVelocity = angularVelocityRandomMode switch
            {
                RandomMode.Fixed => fixedAngularVelocity,
                RandomMode.Random => randomAngularVelocity.Range(),
                _ => fixedAngularVelocity
            };

            return gameObject;
        }
    }
}
