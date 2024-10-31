using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public interface IGroundable
    {
        bool IsGrounded { get; set; }
    }

    public class GroundCheck : MonoBehaviour
    {
        protected IGroundable groundable;

        public void SetGroundable(IGroundable groundable)
        {
            this.groundable = groundable;
        }
    }
}
