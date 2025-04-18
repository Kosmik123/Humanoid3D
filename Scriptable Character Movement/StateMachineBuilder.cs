using System.Collections.Generic;

namespace Bipolar.ScriptableCharacterMovement
{
	public interface IStateMachineBuilder
	{
		StateMachine Build();
	}
	public interface IStateMachineBuilder<TState> : IStateMachineBuilder 
		where TState : State
	{
		IStateMachineBuilder SetInitialState(State initialState);
		IStateMachineBuilder AddTransition(State from, System.Func<bool> trigger, State to, System.Action action = null);
	}

	public class StateMachineBuilder<TState >: IStateMachineBuilder<TState>
		where TState : State
	{
		private State initialState;
		private readonly HashSet<State> states = new HashSet<State>();

		public IStateMachineBuilder SetInitialState(State initialState)
		{
			this.initialState = initialState;
			states.Add(initialState);
			return this;
		}

		public IStateMachineBuilder AddTransition(State from, System.Func<bool> invokingEvent, State to, System.Action action = null)
		{
			from.AddTransition(invokingEvent, to, action);
			states.Add(from);
			states.Add(to);
			return this;
		}

		public StateMachine Build()
		{
			var stateMachine = new StateMachine(initialState);
			foreach (var state in states)
				state.Initialize(stateMachine);
			return stateMachine;
		}
	}

	public class StateMachineBuilder : StateMachineBuilder<State>
	{ }
}
