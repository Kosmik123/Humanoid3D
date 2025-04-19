using System.Collections.Generic;

namespace Bipolar.ScriptableCharacterMovement
{
	internal interface IStateOwner
	{ }

	public partial class State
	{
		internal IStateOwner StateMachine { get; private set; }

		private readonly List<Transition> transitions = new List<Transition>();
		internal IReadOnlyList<Transition> Transitions => transitions;

		internal void Initialize(IStateOwner stateMachine)
		{
			if (StateMachine == null)
				StateMachine = stateMachine;
			else if (StateMachine != stateMachine)
				throw new System.InvalidOperationException($"State {this} is already used by state machine {stateMachine}");
		}

		internal void AddTransition(System.Func<bool> invokingEvent, State to, System.Action action = null) => transitions.Add(new Transition
		{
			invokingEvent = invokingEvent,
			to = to,
			action = action
		});
    }
}
