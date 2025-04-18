using UnityEngine;

namespace Bipolar.ScriptableCharacterMovement
{
	public class WalkingSimulatorStateMachine : MonoBehaviour
	{
		[SerializeField]
		private WalkingState walkingState;

		private StateMachine stateMachine;

		private void Awake()
		{
			var idleState = new IdleState();
			var stateMachineBuilder = new StateMachineBuilder();
			stateMachineBuilder.AddTransition(idleState, () => Input.anyKey, walkingState, () => Debug.Log("Walking"));
			stateMachineBuilder.AddTransition(walkingState, () => !Input.anyKey, idleState, () => Debug.Log("Stopping"));

			stateMachine = stateMachineBuilder.Build();
		}
	}

	[System.Serializable]
	public class IdleState : State
	{ }

	[System.Serializable]
	public class WalkingState : State
	{
		[SerializeField]
		internal float walkSpeed;
	}
}

