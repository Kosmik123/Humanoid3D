using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.FSM
{
	[System.Serializable]
	public struct StateTransition<TState>
	{
		public TState nextState;
		public Predicate predicate;
	}

    public class  State : State<State>
    {
        
    }

    public abstract class State<TSelf> : MonoBehaviour
		where TSelf : State<TSelf>
	{
		[SerializeField]
		private List<StateTransition<TSelf>> transitions;
		public IReadOnlyList<StateTransition<TSelf>> Transitions => transitions;

		public virtual void Enter()
		{ }

		public virtual void Exit()
		{ }

		private void OnValidate()
		{
			if (transitions != null)
			{
				for (int i = transitions.Count - 1; i >= 0; i--)
					if (transitions[i].nextState == this)
						transitions.RemoveAt(i);
			}
		}
	}
}
