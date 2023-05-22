using Bipolar.Humanoid3D.Components;
using UnityEngine;
#if NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif

namespace Bipolar.Humanoid3D.Animation
{
    public class JumpAnimation : HumanoidAnimation
    {
        [SerializeField]
        private Jump jump;

        [SerializeField]
#if NAUGHTY_ATTRIBUTES
        [AnimatorParam(AnimatorName)]
#endif
        private string jumpTriggerName;
        private int jumpTriggerHash;
        public string JumpTriggerName
        {
            get => jumpTriggerName;
            set
            {
                jumpTriggerName = value;
                jumpTriggerHash = Animator.StringToHash(value);
            }
        }

        protected override void Reset()
        {
            base.Reset();
            jump = GetComponent<Jump>();
        }

        private void OnEnable()
        {
            jump.OnJumped += AnimateJump;
            JumpTriggerName = JumpTriggerName;
        }

        private void AnimateJump()
        {
            SetTrigger(jumpTriggerHash);
        }

        private void OnDisable()
        {
            jump.OnJumped -= AnimateJump;
        }
        private void OnValidate()
        {
            JumpTriggerName = JumpTriggerName;
        }
    }   
}
