using UnityEngine;
using UnityEngine.EventSystems;

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

        protected override IStateMachineBuilder GetStateMachineBuilder()
        {
            var idleState = new IdleState();
            var stateMachineBuilder = new StateMachineBuilder()
                .AddTransition(idleState, () => Input.anyKey, walk, () => Debug.Log("Walking"))
                .AddTransition(walk, () => !Input.anyKey, idleState, () => Debug.Log("Stopping"));

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
}

