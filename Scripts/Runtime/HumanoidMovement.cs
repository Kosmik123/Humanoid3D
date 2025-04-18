using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
	public abstract class HumanoidMovement : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 4;

        protected abstract IReadOnlyList<ISpeedModifier> SpeedModifiers { get; }

        internal abstract void CalculateVelocity();
        public abstract Vector3 Velocity { get; }

        protected float GetSpeed()
        {
            float speed = moveSpeed;
            for (int i = 0; i < SpeedModifiers.Count; i++)
            {
                var modifier = SpeedModifiers[i];
                modifier.ModifySpeed(ref speed);
            }

            return speed;
        }
    }
}
