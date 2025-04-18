namespace Bipolar.ScriptableCharacterMovement
{
	internal struct Transition
	{
		public System.Func<bool> invokingEvent;
		public State to;
		public System.Action action;
	}

	public partial class State 
    {
		public void Enter() => OnEnter();

		public void Exit() => OnExit();

		protected virtual void OnEnter()
		{ }

		protected virtual void OnExit()
		{ }
	}
}
