using Bipolar.Humanoid3D.Components;
using UnityEngine;
#if NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif

namespace Bipolar.Humanoid3D.Animation
{
    public class CrouchAnimation : HumanoidAnimation
    {
        [SerializeField]
        private Crouch crouch;

        [SerializeField]
#if NAUGHTY_ATTRIBUTES
        [AnimatorParam(AnimatorName)]
#endif
        private string parameterName;

        protected override void Reset()
        {
            base.Reset();
            crouch = GetComponent<Crouch>();
        }

        private void Update()
        {
            SetBool(Animator.StringToHash(parameterName), crouch.IsCrouching);
        }
    }
}
