using UnityEngine;

namespace Bipolar.FSM
{
    [CreateAssetMenu (menuName ="Bipolar/FSM/FSM Asset")]
    public class FSMAsset : ScriptableObject
    {
        [SerializeField]
        private int stateCount;
    }
}
