using UnityEngine;

namespace Bipolar.Humanoid3D.Components
{
    public class Jump : HumanoidComponent
    {
        public event System.Action OnJumped;

        [SerializeField]
        private float jumpForce = 6;
        public float JumpForce
        {
            get => jumpForce;
            set => jumpForce = value;
        }

        [SerializeField]
        private float coyoteTime = 0.2f;
        public float CoyoteTime
        {
            get => coyoteTime;
            set => coyoteTime = value;
        }

        private float coyoteTimer;


        [SerializeField]
        private float jumpBufferDuration = 0.2f;
        public float JumpBufferDuration
        {
            get => jumpBufferDuration;
            set => jumpBufferDuration = value;
        }

        private float jumpBufferTimer;

        public bool CanJump
        {
            get
            {
                if (humanoid.IsGrounded)
                    return true;

                if (coyoteTimer < coyoteTime)
                    return true;

                return false;
            }
        }

        public bool IsJumpRequested => jumpBufferTimer < jumpBufferDuration;

        public override void DoUpdate(float deltaTime)
        {
            coyoteTimer += deltaTime;
            jumpBufferTimer += deltaTime;

            if (humanoid.IsGrounded)
                coyoteTimer = 0;

            if (UnityEngine.Input.GetKey(KeyCode.Space))
                jumpBufferTimer = 0;

            if (IsJumpRequested && CanJump) 
                DoJump();
        }

        public void DoJump()
        {
            coyoteTimer = coyoteTime;
            humanoid.AddVelocity(Vector3.up * jumpForce);
            OnJumped?.Invoke();
        }


        private void OnValidate()
        {
            if (coyoteTime < 0)
                coyoteTime = 0;
            if (jumpBufferDuration < 0)
                jumpBufferDuration = 0;
        }
    }
}
