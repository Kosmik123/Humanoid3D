using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public class PhysicalDynamicGravity : ScriptableObject, IPhysicalGravity
    {
        public void ApplyGravity(Humanoid<Rigidbody> humanoid)
        {
            humanoid.Body.useGravity = true;
        }
    }
}
