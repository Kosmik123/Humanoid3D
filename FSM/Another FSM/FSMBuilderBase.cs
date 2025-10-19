using UnityEngine;

namespace Bipolar.AnotherFSM
{
    public class FSM : MonoBehaviour
    {
        private StateHolder state;
    }

    public abstract class FSMBuilderBase<TContext> : ScriptableObject
    {
        public abstract void Build(TContext context, FSM fsm);
    }

    public interface IWalkingSimulatorController
    {

    }

    [CreateAssetMenu]
    public class WalkingSimulatorFSMBuilder : FSMBuilderBase<IWalkingSimulatorController>
    {
        public override void Build(IWalkingSimulatorController context, FSM fsm)
        {

        }
    }

    public enum WalkingSimState
    {

    }

    public abstract class StateHolder
    {

    }

    public class StateHolder<TState> : StateHolder
    {
        public TState Current;
    } 
}
