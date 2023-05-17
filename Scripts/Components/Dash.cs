using UnityEngine;
#if NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif

namespace Bipolar.Humanoid3D.Components
{
    public class Dash : HumanoidComponent
    {
        // to będzie musiał być eventowy input provider 
        [SerializeField]
        private KeyCode key = KeyCode.F;

        [SerializeField]
        private float dashSpeed = 20;
        public float DashSpeed
        {
            get => dashSpeed;
            set => dashSpeed = value;
        }

        [SerializeField]
        private float dashDuration = 0.3f;
        public float DashDuration
        {
            get => dashDuration;
            set => dashDuration = value;
        }

        [SerializeField]
        private Transform forwardProvider;
        public Transform ForwardProvider
        {
            get => forwardProvider;
            set => forwardProvider = value;
        }

        [SerializeField]
        private float cooldownDuration = 0.3f;
        public float CooldownDuration
        {
            get => cooldownDuration;
            set => cooldownDuration = value;
        }

#if NAUGHTY_ATTRIBUTES
        [ReadOnly]
#endif
        [SerializeField]
        private float cooldownTimer;

#if NAUGHTY_ATTRIBUTES
        [ReadOnly]
#endif
        [SerializeField]
        private bool cooldownFinished;

#if NAUGHTY_ATTRIBUTES
        [ReadOnly]
#endif
        [SerializeField]
        private bool isDashing;

        private Vector3 dashDirection;

        public override void DoUpdate(float deltaTime)
        {
            if (cooldownFinished == false)
                cooldownTimer += deltaTime;

            if (cooldownTimer > cooldownDuration)
            {
                cooldownFinished = true;
                if (Input.GetKeyDown(key))
                {
                    cooldownTimer = 0;
                    CancelInvoke();
                    isDashing = true;
                    dashDirection = forwardProvider.forward;
                    Invoke(nameof(StopDashing), dashDuration);
                }
            }

            if (isDashing)
                humanoid.AddMovement(dashSpeed * deltaTime * dashDirection);
        }

        public void StopDashing()
        {
            humanoid.AddVelocity(dashSpeed * dashDirection);
            isDashing = false;
            cooldownFinished = false;
            dashDirection = Vector3.zero;
        }
    }
}
