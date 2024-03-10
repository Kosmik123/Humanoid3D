using System;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
    [RequireComponent(typeof(Rigidbody), typeof (CapsuleCollider))]
    public sealed class DynamicHumanoid : Humanoid<Rigidbody>
    {
        private CapsuleCollider _collider;
        public CapsuleCollider Collider
        {
            get
            {
                if (_collider == null)
                    _collider = GetComponent<CapsuleCollider>();
                return _collider;
            }
        }

        public override Vector3 Velocity
        {
            get => Body.velocity;
        }

        public override float Height 
        {
            get => Collider.height;
            set
            {
                Collider.height = value;
                Collider.direction = 1;
            }
        }

        public override float Radius
        {
            get => Collider.radius;
            set
            {
                Collider.radius = value;
            }
        }

        public override Vector3 Center
        {
            get => Collider.center;
            set
            {
                Collider.center = value;
            }
        }

        public override bool IsMoving => throw new NotImplementedException();

        public override Vector3 LocalMovementVelocity => throw new NotImplementedException();

        public override void AddVelocity(Vector3 velocity)
        {
            throw new System.NotImplementedException();
        }

        public override void AddMovementVelocity(Vector3 vector3)
        {
            throw new System.NotImplementedException();
        }

        internal override void ApplyMovement(float deltaTime)
        {
            throw new System.NotImplementedException();
        }
    }
}