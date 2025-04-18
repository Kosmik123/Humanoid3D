using NaughtyAttributes;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
    [AddComponentMenu(Paths.Humanoids + "Character Humanoid")]
    [RequireComponent(typeof(CharacterController))]
    public sealed class CharacterHumanoid : Humanoid<CharacterController>
    {
        [SerializeField, RequireType(typeof(ParticleSystem))]
        private ParticleSystem walkParticles;

        [Space, Header("States")]
        [SerializeField]
        [ReadOnly]
        private Collision collision;
        public CollisionFlags Collision => (CollisionFlags)collision;

        [SerializeField]
        [ReadOnly]
        private Vector3 velocity;
        public override Vector3 Velocity
        {
            get
			{
				return Body.velocity;
            }
        }

        public override Collider Collider => Body;

        [SerializeField]
        [ReadOnly]
        private Vector3 movementVelocity;
        private Vector3 modifiedMovementVelocity;

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

        [SerializeField, ReadOnly]
        private bool isMoving;
        public override bool IsMoving => isMoving;

        public override Vector3 LocalMovementVelocity => Quaternion.Inverse(transform.rotation) * movementVelocity;

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
    }
}
