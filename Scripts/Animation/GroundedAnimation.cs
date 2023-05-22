using UnityEngine;

namespace Bipolar.Humanoid3D.Animation
{
    public class GroundedAnimation : HumanoidAnimation
    {
        [SerializeField]
        private Humanoid humanoid;
        [SerializeField]
        private string groundedParameterName;

        protected override void Reset()
        {
            base.Reset();
            humanoid = GetComponent<Humanoid>();
        }

        private void OnEnable()
        {
            humanoid.OnGroundedChanged += AnimateGrounded;
        }

        private void AnimateGrounded(bool isGrounded)
        {
            SetBool(Animator.StringToHash(groundedParameterName), isGrounded);
        }

        private void OnDisable()
        {
            humanoid.OnGroundedChanged -= AnimateGrounded;
        }
    }
}
