using UnityEngine;

namespace Bipolar.Humanoid3D
{
    [System.Serializable]
    public class Gravity
    {
        [field: SerializeField, Tooltip("Gravity scale when going up")]
        public float UpScale { get; set; } = 1;

        [field: SerializeField, Tooltip("Gravity scale when falling down")]
        public float DownScale { get; set; } = 1;
    }

    public abstract class Humanoid : MonoBehaviour
    {
        public event System.Action<bool> OnGroundedChanged;

        [field: SerializeField]
        public Gravity Gravity { get; set; }

        private bool isGrounded;
        public bool IsGrounded 
        { 
            get => isGrounded;
            set
            {
                isGrounded = value;
                OnGroundedChanged?.Invoke(value);
            }
        }

        public abstract Vector3 Center { get; set; }
        public abstract float Height { get; set; }
        public abstract float Radius { get; set; }

        public abstract Vector3 Velocity { get; }
        public abstract Vector3 LocalMovementVelocity { get; }
        public abstract bool IsMoving { get; }

        public abstract void AddVelocity(Vector3 velocity);
        public abstract void AddMovementVelocity(Vector3 motion);
        
        internal abstract void ApplyMovement(float deltaTime);
        internal abstract void ApplyGravity(float deltaTime);

        protected const float defaultHumanHeight = 1.8f;

        protected virtual void Awake()
        {
        }
    }
}
