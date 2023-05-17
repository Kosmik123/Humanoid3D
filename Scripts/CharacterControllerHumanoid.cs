using System;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class CharacterControllerHumanoid : Humanoid
    {
        public override event Action<bool> OnGroundedChanged;
     
        [Header("States")]
        [SerializeField]
        private Collision collision;
        public CollisionFlags Collision => (CollisionFlags)collision;
        public override bool IsGrounded => collision.HasFlag((Collision)CollisionFlags.Below);

        [SerializeField]
        private Vector3 velocity;
        public override Vector3 Velocity
        {
            get => Character.velocity;
        }

        [SerializeField]
        private Vector3 motion;
        private Vector3 modifiedMotion;
        public Vector3 Motion
        {
            get => motion;
        }


        private CharacterController character;
        private CharacterController Character
        {
            get
            {
                if (character == null)
                    character = GetComponent<CharacterController>();
                return character;
            }
        }

        public override float Height
        {
            get => Character.height;
            set => Character.height = value;
        }

        public override float Radius
        {
            get => Character.radius;
            set => Character.radius = value;
        }

        public override Vector3 Center
        {
            get => Character.center;
            set => Character.center = value;
        }

        private void Reset()
        {
            character = GetComponent<CharacterController>();
            Center = Vector3.up * defaultHumanHeight / 2;
            Height = defaultHumanHeight;
        }

        private void Awake()
        {
            character = GetComponent<CharacterController>();
        }

        internal override void ApplyMovement(float deltaTime)
        {
            motion = modifiedMotion;

            var localMotion = motion + velocity * deltaTime;
            bool previousGrounded = IsGrounded;
            collision = (Collision)(int)Character.Move(localMotion);
            HandleCeilingHit();
            modifiedMotion = Vector3.zero;
            if (IsGrounded != previousGrounded)
                OnGroundedChanged?.Invoke(IsGrounded);
        }

        private void HandleCeilingHit()
        {
            if (collision == Humanoid3D.Collision.Above && velocity.y > 0)
                velocity.y = 0;
        }

        public override void AddMovement(Vector3 motion)
        {
            modifiedMotion += motion;
        }

        public override void AddVelocity(Vector3 velocity)
        {
            this.velocity += velocity;
        }

        internal override void ApplyGravity(float deltaTime)
        {
            if (IsGrounded && velocity.y < 0)
            {
                velocity = 0.2f * Gravity.UpScale * Physics.gravity;
            }
            else
            {
                float gravityScale = Velocity.y > 0 ? Gravity.UpScale : Gravity.DownScale;
                velocity += gravityScale * deltaTime * Physics.gravity;
            }
        }

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            
        }
    }
}
