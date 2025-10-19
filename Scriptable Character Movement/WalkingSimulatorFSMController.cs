using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.ScriptableCharacterMovement
{
	public abstract class FSMController : MonoBehaviour
	{
		protected StateMachine stateMachine;

		protected virtual void Awake()
		{
			stateMachine = GetStateMachineBuilder().Build();
		}

		protected abstract IStateMachineBuilder GetStateMachineBuilder();
	}

	public abstract class FSMController<TState> : FSMController
		where TState : State
	{
	}

	[RequireComponent(typeof(CharacterController))]
	public class WalkingSimulatorFSMController : FSMController
	{
		private CharacterController characterController;

		protected override void Awake()
		{
			base.Awake();
			characterController = GetComponent<CharacterController>();
		}

		[SerializeField]
		private WalkingState walk;
		[SerializeField]
		private State jump;

		protected override IStateMachineBuilder GetStateMachineBuilder()
		{
			var idle = new IdleState();

			var stateMachineBuilder = new StateMachineBuilder()
				.Add(Transitions.From(idle)
					.Add(() => Input.GetButton("Jump"), jump)
					.Add(() => Input.GetAxis("Horizontal") != 0, walk))
				.Add(Transitions.From(walk)
					.Add(() => Input.GetButton("Jump"), jump)
					.Add(() => !Input.anyKey, idle));



			return stateMachineBuilder;






		}
	}

	[System.Serializable]
	public class IdleState : State
	{ }

	[System.Serializable]
	public class WalkingState : State, IUpdatedState
	{
		[SerializeField]
		public float walkSpeed;

		void IUpdatedState.Update()
		{
			float horizontal = Input.GetAxis("Horizontal");
			float vertical = Input.GetAxis("Vertical");
		}
	}

	public class Transitions : Transitions<State>
	{
		protected Transitions(State from) : base(from)
		{ }
	}

	public class Transitions<TState>
		where TState : State
	{
		private readonly TState from;
		private readonly List<Transition> transitions = new List<Transition>();

		protected Transitions(TState from)
		{
			this.from = from;
		}

		public static Transitions<TState> From(TState from) => new Transitions<TState>(from);
	
		public Transitions<TState> Add(System.Func<bool> invokingEvent, TState to, System.Action action = null)
		{
			transitions.Add(new Transition(invokingEvent, to, action));
			return this;
		}
	}
}

