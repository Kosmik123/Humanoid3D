using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public interface IHumanoid
    {
        Vector3 Center { get; set; }
        float Height { get; set; }
        float Radius { get; set; }

        Transform Transform { get; }
        Collider Collider { get; }
        Vector3 Velocity { get; }
        bool IsGrounded { get; }

        void AddVelocity(Vector3 velocity);
    }

    public abstract class Humanoid : MonoBehaviour, IHumanoid, IGroundable
    {
        public event System.Action<bool> OnGroundedChanged;

        [SerializeField]
        private GroundCheck groundCheck;

        [SerializeField]
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
        
        public Transform Transform => transform;
        
        public abstract Vector3 Center { get; set; }
        public abstract float Height { get; set; }
        public abstract float Radius { get; set; }

        public abstract Vector3 Velocity { get; }
        public abstract Vector3 LocalMovementVelocity { get; }
        public abstract bool IsMoving { get; }

        public abstract Collider Collider { get; }

        public abstract void AddVelocity(Vector3 velocity);
        public abstract void AddMovementVelocity(Vector3 motion);
        
        internal abstract void ApplyMovement(float deltaTime);
        internal abstract void ApplyGravity();

        public const float defaultHumanHeight = 1.8f;
        public const float defaultEyesHeight = defaultHumanHeight - 0.1f;

        protected virtual void Reset()
        {
            Center = Vector3.up * defaultHumanHeight / 2;
            Height = defaultHumanHeight;
        }

        protected virtual void Awake()
        { 
            if (groundCheck)
                groundCheck.SetGroundable(this);
        }
    }

    public interface IHumanoid<out TBody> : IHumanoid
    {
        TBody Body { get; }
    }

    public abstract class Humanoid<TBody> : Humanoid, IHumanoid<TBody>
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
        private IGravity<IHumanoid<TBody>> _gravity;
        public IGravity<IHumanoid<TBody>> Gravity
        {
            get
            {
                if (gravity == null)
                    return null;
                _gravity ??= gravity as IGravity<IHumanoid<TBody>>;
                return _gravity;
            }
            set
            {
                gravity = (Object)value;
                _gravity = gravity as IGravity<IHumanoid<TBody>>;
            }
        }

        [SerializeField, RequireInterface(typeof(IHumanoidComponent))]
        private List<BaseHumanoidComponent> humanoidComponents;
        private readonly List<IHumanoidComponent<IHumanoid<TBody>>> _components = new List<IHumanoidComponent<IHumanoid<TBody>>>();
        public IReadOnlyList<IHumanoidComponent<IHumanoid<TBody>>> Components
        {
            get
            {
                if (_components.Count != humanoidComponents.Count)
                    ValidateComponents();
                return _components;
            }
        }

        private void ValidateComponents()
        {
            _components.Clear();
            for (int i = humanoidComponents.Count - 1; i >= 0; i--)
            {
                if (humanoidComponents[i] == null)
                    continue;

                if (humanoidComponents[i] is IHumanoidComponent<IHumanoid<TBody>> typedComponent)
                {
                    _components.Add(typedComponent);
                }
                else
                {
                    humanoidComponents[i] = null;
                }
            }
        }

        protected virtual void OnEnable()
        {
            ValidateComponents();
            InitializeComponents();
        }

        private void Update()
        {
            ApplyGravity();
            foreach (var component in Components)
                if (component != null)
                    component.Apply();
        }

        internal override void ApplyGravity()
        {
            if (gravity != null)
                Gravity.ApplyGravity(this);
        }

        protected virtual void OnValidate()
        {
            if (Body == null)
                gameObject.AddComponent<TBody>();

            Gravity = Gravity;
            ValidateComponents();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            foreach (var component in Components)
                if (component != null)
                    component.SetHumanoid(this);
        }
    }
}
