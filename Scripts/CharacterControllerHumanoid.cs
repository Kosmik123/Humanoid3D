using System;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class CharacterControllerHumanoid : Humanoid
    {
        [Header("States")]
        [SerializeField]
        private Collision collision;
        public CollisionFlags Collision => (CollisionFlags)collision;

        [SerializeField]
        private Vector3 velocity;
        public override Vector3 Velocity
        {
            get => Character.velocity;
        }

        [SerializeField]
        private Vector3 movementVelocity;
        private Vector3 modifiedMovementVelocity;
        public Vector3 Motion
        {
            get => movementVelocity;
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

        [SerializeField]
        private bool isMoving;
        public override bool IsMoving => isMoving;

        private void Reset()
        {
            character = GetComponent<CharacterController>();
            Center = Vector3.up * defaultHumanHeight / 2;
            Height = defaultHumanHeight;
        }

        protected override void Awake()
        {
            base.Awake();
            character = GetComponent<CharacterController>();
        }

        internal override void ApplyMovement(float deltaTime)
        {
            movementVelocity = modifiedMovementVelocity;

            var localMotion = (transform.rotation * movementVelocity + velocity) * deltaTime;
            collision = (Collision)Character.Move(localMotion);
            HandleCeilingHit();
            isMoving = movementVelocity != Vector3.zero;
            modifiedMovementVelocity = Vector3.zero;
            IsGrounded = collision.HasFlag((Collision)CollisionFlags.Below);
        }

        private void HandleCeilingHit()
        {
            if (collision == Humanoid3D.Collision.Above && velocity.y > 0)
                velocity.y = 0;
        }

        public override void AddMovementVelocity(Vector3 motion)
        {
            modifiedMovementVelocity += motion;
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
