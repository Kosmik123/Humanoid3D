using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public class InAirSpeedModifier : MonoBehaviour, ISpeedModifier
    {
        [SerializeField]
        private Humanoid humanoid;
        [SerializeField]
        private float inAirModifier = 0.5f;

        public void ModifySpeed(ref float speed)
        {
            if (humanoid.IsGrounded == false)
                speed *= inAirModifier;
        }
    }
}
