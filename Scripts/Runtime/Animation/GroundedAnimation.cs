using UnityEngine;

namespace Bipolar.Humanoid3D.Animation
{
    [AddComponentMenu(AddComponentPath.Animation + "Grounded Animation")]
    public class GroundedAnimation : HumanoidAnimation
    {
        [SerializeField]
        private Humanoid humanoid;

#if NAUGHTY_ATTRIBUTES
        [AnimatorParameter(AnimatorName, AnimatorControllerParameterType.Bool)]
#endif
        private AnimationParameter groundedParameterName;

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
            SetBool(groundedParameterName, isGrounded);
        }

        private void OnDisable()
        {
            humanoid.OnGroundedChanged -= AnimateGrounded;
        }
    }
}
