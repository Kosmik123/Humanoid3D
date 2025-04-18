
using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public class SphereGroundCheck : GroundCheck
    {
        [SerializeField]
        private LayerMask groundLayers = ~0;

        [SerializeField]
        private float radius = 0.2f;

        private void Update()
        {
            groundable.IsGrounded = Physics.CheckSphere(transform.position, radius, groundLayers);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
