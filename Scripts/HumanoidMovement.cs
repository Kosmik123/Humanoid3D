using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public interface ISpeedModifier
    {
        void ModifySpeed(ref float speed);
    }

    public abstract class HumanoidMovement : MonoBehaviour
    {
        [SerializeField]
        private Transform forwardProvider;
        public Transform ForwardProvider
        {
            get => forwardProvider;
            set => forwardProvider = value;
        }

        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        protected abstract IReadOnlyList<ISpeedModifier> SpeedModifiers { get; }

        internal abstract void CalculateMotion(float deltaTime);

        public abstract Vector3 Motion { get; }

        protected float GetSpeed()
        {
            float speed = moveSpeed;
            foreach (var modifier in SpeedModifiers)
                modifier.ModifySpeed(ref speed);

            return speed;
        }
    }
}
