using System;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
    [RequireComponent(typeof(Rigidbody), typeof (CapsuleCollider))]
    public sealed class RigidbodyHumanoid : Humanoid
    {
        public override event Action<bool> OnGroundedChanged;

        private new Rigidbody rigidbody;
        private new CapsuleCollider collider;

        public override Vector3 Velocity
        {
            get => rigidbody.velocity;
        }

        public override float Height 
        {
            get => collider.height;
            set
            {
                collider.height = value;
                collider.direction = 1;
            }
        }

        public override float Radius
        {
            get => collider.radius;
            set
            {
                collider.radius = value;
            }
        }

        public override Vector3 Center
        {
            get => collider.center;
            set
            {
                collider.center = value;
            }
        }

        public override bool IsGrounded => throw new System.NotImplementedException();

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<CapsuleCollider>();
        }

        public override void AddVelocity(Vector3 velocity)
        {
            throw new System.NotImplementedException();
        }

        public override void AddMovement(Vector3 vector3)
        {
            throw new System.NotImplementedException();
        }

        internal override void ApplyMovement(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        internal override void ApplyGravity(float deltaTime)
        {
            float gravityScale = Velocity.y > 0 ? Gravity.UpScale : Gravity.DownScale;
            rigidbody.AddRelativeForce(Physics.gravity * gravityScale);


        }

    }
}