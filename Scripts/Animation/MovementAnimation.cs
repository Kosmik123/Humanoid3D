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
        private string speedParameterName;
        [SerializeField]
        private float minSpeedValue = 0.1f;

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
            if (isMoving)
            {
                Vector2 horizontalVelocity = new Vector2(humanoid.Velocity.x, humanoid.Velocity.z);
                float speed = horizontalVelocity.magnitude;
                SetFloat(Animator.StringToHash(speedParameterName), speed);
            }
            else
            {
                SetFloat(Animator.StringToHash(speedParameterName), 1);
            }
        }
    }
}
