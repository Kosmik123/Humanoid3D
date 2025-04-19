namespace Bipolar.ScriptableCharacterMovement
{
    internal struct Transition
    {
        public System.Func<bool> invokingEvent;
        public State to;
        public System.Action action;

        public Transition(System.Func<bool> invokingEvent, State to, System.Action action)
        {
            this.invokingEvent = invokingEvent;
            this.to = to;
            this.action = action;
        }
    }

    public interface IEnterableState
    {
        void Enter();
    }

    public interface IExitableState
    {
        void Exit();
    }

    public interface IUpdatedState
    {
        void Update();
    }

    public interface IFixedUpdatedState
    {
        void FixedUpdate();
    }

    public partial class State
    { }
}
