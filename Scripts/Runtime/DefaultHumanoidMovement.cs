using System.Collections.Generic;
using UnityEngine;
#if NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif

namespace Bipolar.Humanoid3D
{
    public class DefaultHumanoidMovement : HumanoidMovement
    {
        [SerializeField]
        private MoveInputProvider moveInputProvider;

        [SerializeField]
        private List<SpeedModifier> speedModifiers;
        protected override IReadOnlyList<ISpeedModifier> SpeedModifiers => speedModifiers;

        [SerializeField, Range(0, 1)]
        private float sideModifier = 1;
        [SerializeField, Range(0, 1)]
        private float backModifier = 1;

        private Vector3 velocity;
        public override Vector3 Velocity => velocity;

#if NAUGHTY_ATTRIBUTES
        [NaughtyAttributes.ShowNonSerializedField]
#endif
        private float currentSpeed;

        internal override void CalculateVelocity()
        {
            currentSpeed = GetSpeed();

            Vector3 velocity = currentSpeed * GetMovementDirection();
            if (velocity.sqrMagnitude > currentSpeed * currentSpeed)
                velocity = velocity.normalized * currentSpeed;

            this.velocity = velocity;
        }

        private Vector3 GetMovementDirection()
        {
            var moveInput = moveInputProvider.GetMovement();
            float x = moveInput.x * sideModifier;
            float z = moveInput.y;
            if (z < 0)
                z *= backModifier;

            return transform.forward * z + transform.right * x;
        }
    }
}
