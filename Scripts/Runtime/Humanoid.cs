using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
	public abstract class Humanoid : MonoBehaviour, IGroundable
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

		[System.Serializable]
		public class Gravity<TGravityBody> : Serialized<IGravity<Humanoid<TGravityBody>>>, IGravity<Humanoid<TGravityBody>>
			where TGravityBody : Component
		{
			public void ApplyGravity(Humanoid<TGravityBody> humanoid)
			{
				Value.ApplyGravity(humanoid);
			}
		}

		[SerializeField]
		private Gravity<TBody> gravity;

		[SerializeField, RequireInterface(typeof(IHumanoidComponent))]
		private List<BaseHumanoidComponent> humanoidComponents;
		private readonly List<IHumanoidComponent<Humanoid<TBody>>> _components = new List<IHumanoidComponent<Humanoid<TBody>>>();
		public IReadOnlyList<IHumanoidComponent<Humanoid<TBody>>> Components
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
			int count = humanoidComponents?.Count ?? 0;
			for (int i = count - 1; i >= 0; i--)
			{
				if (humanoidComponents[i] == null)
					continue;

				if (humanoidComponents[i] is IHumanoidComponent<Humanoid<TBody>> typedComponent)
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
			gravity?.Value.ApplyGravity(this);
		}

		protected virtual void OnValidate()
		{
			if (Body == null)
				gameObject.AddComponent<TBody>();

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
