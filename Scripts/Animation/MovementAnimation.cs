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
        private HumanoidMovement movement;

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
        private Humanoid humanoid;

        public bool IsMoving => humanoid == null || humanoid.IsMoving;

        protected override void Reset()
        {
            base.Reset();
            movement = GetComponent<HumanoidMovement>();
        }

        private void Update()
        {
            bool isMoving = IsMoving;
            SetBool(Animator.StringToHash(movingParameterName), isMoving);

            Vector2 horizontalVelocity = new Vector2(humanoid.Velocity.x, humanoid.Velocity.z);
            horizontalVelocity.Scale(speedModifiers);
            SetFloat(Animator.StringToHash(forwardSpeedParameterName), horizontalVelocity.y);
            SetFloat(Animator.StringToHash(sideSpeedParameterName), horizontalVelocity.x);
        }
    }
}
