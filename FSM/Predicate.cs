namespace Bipolar.FSM
{
    public interface IPredicate
    {
        bool Evaluate();    
    }

	[System.Serializable]
	public class Predicate : Serialized<IPredicate>, IPredicate
	{
		public bool Evaluate() => Value.Evaluate();
	}
}
