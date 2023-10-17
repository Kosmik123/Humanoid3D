using UnityEngine;
#if NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif

namespace Bipolar.Humanoid3D.Animation
{
    public class MovementAnimation : HumanoidAnimation
    {
        [Header("Settings")]
        [SerializeField]
        private Humanoid humanoid;
        [SerializeField, Range(0, 1)]
        private float smoothing;

        [SerializeField]
#if NAUGHTY_ATTRIBUTES
        [AnimatorParam(AnimatorName)]
#endif
        private string movingParameterName;

        [SerializeField]
#if NAUGHTY_ATTRIBUTES
        [AnimatorParam(AnimatorName)]
#endif
        private string forwardSpeedParameterName;

        [SerializeField]
#if NAUGHTY_ATTRIBUTES
        [AnimatorParam(AnimatorName)]
#endif
        private string sideSpeedParameterName;

        [SerializeField]
        private Vector2 speedModifiers = Vector2.one;

        [Header("Humanoid Feedback")]
        [SerializeField]
        private bool animateNotMoving;

        [SerializeField]
        private Vector2 horizontalVelocity;

        public bool IsMoving => humanoid == null || humanoid.IsMoving;

        protected override void Reset()
        {
            base.Reset();
        }

        private void Update()
        {
            bool isMoving = IsMoving;
            SetBool(Animator.StringToHash(movingParameterName), isMoving);

            horizontalVelocity = Vector2.Lerp(
                new Vector2(humanoid.LocalMovementVelocity.x, humanoid.LocalMovementVelocity.z),
                horizontalVelocity, smoothing);

            horizontalVelocity.Scale(speedModifiers);
            SetFloat(Animator.StringToHash(forwardSpeedParameterName), horizontalVelocity.y);
            SetFloat(Animator.StringToHash(sideSpeedParameterName), horizontalVelocity.x);
        }
    }
}
