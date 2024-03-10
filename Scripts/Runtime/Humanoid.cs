using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public abstract class Humanoid : MonoBehaviour
    {
        public event System.Action<bool> OnGroundedChanged;

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

        public const float defaultHumanHeight = 1.8f;
        public const float defaultEyesHeight = defaultHumanHeight - 0.1f;

        protected virtual void Awake()
        { }
    }

    public abstract class Humanoid<TBody> : Humanoid
        where TBody : Component
    {
        private TBody _body;
        public TBody Body
        {
            get
            {
                if (_body == null)
                    _body = GetComponent<TBody>();
                return _body; 
            }
        }

        [SerializeField, RequireInterface(typeof(IGravity))]
        protected Object gravity;
        public IGravity<TBody> Gravity
        {
            get => gravity as IGravity<TBody>;
            set => gravity = (Object)value;
        }

        [SerializeField]
        private HumanoidComponent[] components;

        internal override void ApplyGravity(float deltaTime)
        {
            if (Gravity != null)
                Gravity.ApplyGravity(this);
        }

        protected virtual void OnValidate()
        {
            Gravity = Gravity;
        }
    }
}
