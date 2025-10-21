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
		IStateMachineBuilder<TState> SetInitialState(TState initialState);
		IStateMachineBuilder<TState> AddTransition(TState from, System.Func<bool> trigger, TState to, System.Action action = null);
		IStateMachineBuilder<TState> Add(Transitions<TState> transitions);
	}

	public class StateMachineBuilder<TState> : IStateMachineBuilder<TState>
		where TState : State
	{
		private State initialState;
		private readonly HashSet<TState> states = new HashSet<TState>();

		public IStateMachineBuilder<TState> SetInitialState(TState initialState)
		{
			this.initialState = initialState;
			states.Add(initialState);
			return this;
		}

		public IStateMachineBuilder<TState> AddTransition(TState from, System.Func<bool> invokingEvent, TState to, System.Action action = null)
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

		public IStateMachineBuilder<TState> Add(Transitions<TState> transitions)
		{
			return default;
		}
	}

	public class StateMachineBuilder : StateMachineBuilder<State>
	{ }

	public class TransititionBuilder
	{

	}

	public static class StateMachineBuilderExtensions<TState> 
		where TState : State
	{
	}
}
