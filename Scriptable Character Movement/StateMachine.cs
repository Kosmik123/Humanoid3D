using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.ScriptableCharacterMovement
{
    public sealed class StateMachine : IStateOwner, System.IDisposable
    {
        private State currentState;
        public State CurrentState => currentState;

        private State initialState;


        private IUpdatedState currentUpdated;
        private IFixedUpdatedState currentFixedUpdated;

        public bool IsStarted { get; private set; } = false;

        internal StateMachine(State initialState)
        {
            currentState = initialState;
        }

        public void Start()
        {
            if (initialState == null)
            {
                Debug.LogError("Initial state must be set before starting State Machine");
                return;
            }

            ChangeState(initialState);
            StateMachineRunner.Instance.Register(this);
            IsStarted = true;
        }

        public void SetInitialState(State initialState)
        {
            if (IsStarted)
            {
                Debug.LogError("Initial state must be set before starting State Machine");
                return;
            }

            this.initialState = initialState;
        }

        public void ForceSetState(State state)
        {
            Debug.LogWarning($"Setting state {state} forced");
            ChangeState(state);
        }

        private void ChangeState(State state, System.Action withAction = null)
        {
            if (currentState is IExitableState exitable)
                exitable.Exit();

            withAction?.Invoke();
            currentState = state;
            currentUpdated = currentState as IUpdatedState;
            currentFixedUpdated = currentState as IFixedUpdatedState;

            if (currentState is IEnterableState enterable)
                enterable.Enter();
        }

        private void Update()
        {
            HandleTransitions();
            currentUpdated?.Update();
        }

        private void HandleTransitions()
        {
            for (int i = 0; i < currentState.Transitions.Count; i++)
            {
                var transition = currentState.Transitions[i];
                if (transition.invokingEvent.Invoke())
                {
                    ChangeState(transition.to, transition.action);
                    return;
                }
            }
        }

        private void FixedUpdate()
        {
            currentFixedUpdated?.FixedUpdate();
        }

        public void Dispose()
        {
            StateMachineRunner.Instance.Unregister(this);
        }

        internal class StateMachineRunner : MonoBehaviour
        {
            private static StateMachineRunner instance;
            internal static StateMachineRunner Instance
            {
                get
                {
                    if (instance == null)
                        CreateInstance();
                    return instance;
                }
            }

            private static void CreateInstance()
            {
                new GameObject("State Machine Runner").AddComponent<StateMachineRunner>();
            }

            private readonly List<StateMachine> updatedStateMachines = new List<StateMachine>();

            private void Awake()
            {
                if (instance == null)
                    instance = this;
                else if (instance != this)
                    Destroy(this);
            }

            public void Register(StateMachine stateMachine) => updatedStateMachines.Add(stateMachine);

            public void Unregister(StateMachine stateMachine) => updatedStateMachines.Remove(stateMachine);

            private void Update()
            {
                for (int i = 0; i < updatedStateMachines.Count; i++)
                {
                    updatedStateMachines[i].Update();
                }
            }

            private void OnDestroy()
            {
                if (instance == this)
                    instance = null;
            }
        }
    }





    public abstract class GenericStateMachine<TSelf> : MonoBehaviour
        where TSelf : GenericStateMachine<TSelf>
    {
        [SerializeField]
        private StateMachineConfigurator<TSelf> machineConfigurator;

        protected virtual void Awake()
        {
            machineConfigurator.Configure((TSelf)this);
        }
    }

    public abstract class StateMachineConfigurator<TStateMachine> : ScriptableObject
        where TStateMachine : GenericStateMachine<TStateMachine>
    {
        public abstract void Configure(TStateMachine stateMachine);
    }


    public class SoulslikeCharacterController : GenericStateMachine<SoulslikeCharacterController>
    {

    }

    public class SoulslikeCharacterControllerConfigurator : StateMachineConfigurator<SoulslikeCharacterController>
    {
        public override void Configure(SoulslikeCharacterController stateMachine)
        {

        }
    }





}
