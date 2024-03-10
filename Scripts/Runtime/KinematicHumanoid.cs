using UnityEngine;

namespace Bipolar.Humanoid3D
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class KinematicHumanoid : Humanoid<CharacterController>
    {
        [Header("States")]
        [SerializeField]
        private Collision collision;
        public CollisionFlags Collision => (CollisionFlags)collision;

        [SerializeField]
        private Vector3 velocity;
        public override Vector3 Velocity
        {
            get => Body.velocity;
        }

        [SerializeField]
        private Vector3 movementVelocity;
        private Vector3 modifiedMovementVelocity;
        public Vector3 Motion
        {
            get => movementVelocity;
        }

        public override float Height
        {
            get => Body.height;
            set => Body.height = value;
        }

        public override float Radius
        {
            get => Body.radius;
            set => Body.radius = value;
        }

        public override Vector3 Center
        {
            get => Body.center;
            set => Body.center = value;
        }

        [SerializeField]
        private bool isMoving;
        public override bool IsMoving => isMoving;

        public override Vector3 LocalMovementVelocity => Quaternion.Inverse(transform.rotation) * movementVelocity;

        private void Reset()
        {
            Center = Vector3.up * defaultHumanHeight / 2;
            Height = defaultHumanHeight;
        }

        internal override void ApplyMovement(float deltaTime)
        {
            movementVelocity = modifiedMovementVelocity;

            var localMotion = (movementVelocity + velocity) * deltaTime;
            collision = (Collision)Body.Move(localMotion);
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
            base.ApplyGravity(deltaTime);
            //if (IsGrounded && velocity.y < 0)
            //{
            //    velocity = 0.2f * GravityStruct.UpScale * Physics.gravity;
            //}
            //else
            //{
            //    float gravityScale = Velocity.y > 0 ? GravityStruct.UpScale : GravityStruct.DownScale;
            //    velocity += gravityScale * deltaTime * Physics.gravity;
            //}
        }
    }
}
