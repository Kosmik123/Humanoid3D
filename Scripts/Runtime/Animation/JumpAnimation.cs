using Bipolar.Humanoid3D.Components;
using UnityEngine;

namespace Bipolar.Humanoid3D.Animation
{
    [AddComponentMenu(AddComponentPath.Animation + "Jump Animation")]
    public class JumpAnimation : HumanoidAnimation
    {
        [SerializeField]
        private Jump jump;

        [SerializeField]
#if NAUGHTY_ATTRIBUTES
        [AnimatorParameter(AnimatorName, AnimatorControllerParameterType.Trigger)]
#endif
        private AnimationParameter jumpTriggerName;

        protected override void Reset()
        {
            base.Reset();
            jump = GetComponent<Jump>();
        }

        private void OnEnable()
        {
            jump.OnJumped += AnimateJump;
        }

        private void AnimateJump()
        {
            SetTrigger(jumpTriggerName);
        }

        private void OnDisable()
        {
            jump.OnJumped -= AnimateJump;
        }
    }   
}
