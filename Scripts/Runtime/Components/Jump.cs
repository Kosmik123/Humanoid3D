using UnityEngine;

namespace Bipolar.Humanoid3D.Components
{
	[AddComponentMenu(Paths.Input + "Jump")]
	public class Jump : MonoBehaviour, IHumanoidComponent<Humanoid>
	{
		public event System.Action OnJumped;

		[SerializeField]
		private float jumpForce = 6;
		public float JumpForce
		{
			get => jumpForce;
			set => jumpForce = value;
		}

		[SerializeField]
		private float coyoteTime = 0.2f;
		public float CoyoteTime
		{
			get => coyoteTime;
			set => coyoteTime = value;
		}
		private float coyoteTimer;

		[SerializeField]
		private float jumpBufferDuration = 0.2f;
		public float JumpBufferDuration
		{
			get => jumpBufferDuration;
			set => jumpBufferDuration = value;
		}
		private float jumpBufferTimer;

		public bool CanJump(Humanoid humanoid)
		{
			if (humanoid.IsGrounded)
				return true;

			if (coyoteTimer < coyoteTime)
				return true;

			return false;
		}

		public bool IsJumpRequested => jumpBufferTimer <= jumpBufferDuration;

		protected void OnEnable()
		{
			coyoteTimer = coyoteTime;
			jumpBufferTimer = jumpBufferDuration;
		}

		public void Apply(Humanoid humanoid)
		{
			coyoteTimer += Time.deltaTime;
			jumpBufferTimer += Time.deltaTime;

			if (humanoid.IsGrounded)
				coyoteTimer = 0;

			if (UnityEngine.Input.GetKeyDown(KeyCode.Space)) // Must be changed
				jumpBufferTimer = 0;

			if (IsJumpRequested && CanJump(humanoid))
				DoJump(humanoid);
		}

		public void DoJump(Humanoid humanoid)
		{
			coyoteTimer = coyoteTime;
			jumpBufferTimer = jumpBufferDuration;
			humanoid.AddVelocity(humanoid.Transform.up * jumpForce);
			OnJumped?.Invoke();
		}

		private void OnValidate()
		{
			if (coyoteTime < 0)
				coyoteTime = 0;
			if (jumpBufferDuration < 0)
				jumpBufferDuration = 0;
		}
	}
}
