using System;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
    [AddComponentMenu(AddComponentPath.Humanoids + "Physical Humanoid")]
    [RequireComponent(typeof(Rigidbody), typeof (CapsuleCollider))]
    public sealed class PhysicalHumanoid : Humanoid<Rigidbody>
    {
        private CapsuleCollider _collider;
        public CapsuleCollider CapsuleCollider
        {
            get
            {
                if (_collider == null)
                    _collider = GetComponent<CapsuleCollider>();
                return _collider;
            }
        }

        public override Collider Collider => CapsuleCollider;

        public override Vector3 Velocity
        {
            get => Body.velocity;
        }

        public override float Height 
        {
            get => CapsuleCollider.height;
            set
            {
                CapsuleCollider.height = value;
                CapsuleCollider.direction = 1;
            }
        }

        public override float Radius
        {
            get => CapsuleCollider.radius;
            set
            {
                CapsuleCollider.radius = value;
            }
        }

        public override Vector3 Center
        {
            get => CapsuleCollider.center;
            set
            {
                CapsuleCollider.center = value;
            }
        }

        public override bool IsMoving => throw new NotImplementedException();

        public override Vector3 LocalMovementVelocity => throw new NotImplementedException();

        public override void AddVelocity(Vector3 velocity)
        {
            var current = Body.velocity;
            current += velocity;
            Body.velocity = current;
        }

        public override void AddMovementVelocity(Vector3 vector3)
        {
        }

        internal override void ApplyMovement(float deltaTime)
        {
        }
    }
}