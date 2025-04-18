using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.FSM
{
	public class StateMachine : StateMachine<State>
	{

	}
	public class StateMachine<TState>: MonoBehaviour
		where TState : State<TState>
	{
		[SerializeField]
		private TState initialState;

		private TState currentState;

		private readonly List<StateTransition<TState>> listenedTransitions = new List<StateTransition<TState>>();

		private void Start()
		{
			ChangeState(initialState);
		}

		public void ChangeState(TState state, bool force = false)
		{
			if (force == false && currentState == state)
				return;

			if (currentState != null)
			{
				currentState.Exit();
			}

			currentState = state;

			listenedTransitions.Clear();
			listenedTransitions.AddRange(currentState.Transitions);
			currentState.Enter();
		}

		private void Update()
		{
			foreach (var transition in listenedTransitions)
			{
				if (transition.predicate.Evaluate())
				{
					ChangeState(transition.nextState);
					break;
				}
			}
		}

		private void OnValidate()
		{

		}
	}
}
