using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public class CollisionFlagsGroundCheck : GroundCheck
    {
        [SerializeField]
        private CharacterController characterController;

        private void Update()
        {
            groundable.IsGrounded = characterController.collisionFlags.HasFlag(CollisionFlags.Below);
        }
    }
}
