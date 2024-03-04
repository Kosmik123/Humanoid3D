﻿using Bipolar.Humanoid3D.Components;
using UnityEngine;

namespace Bipolar.Humanoid3D.Animation
{
    public class CrouchAnimation : HumanoidAnimation
    {
        [SerializeField]
        private Crouch crouch;

        [SerializeField]
#if NAUGHTY_ATTRIBUTES
        [NaughtyAttributes.AnimatorParam(AnimatorName)]
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
