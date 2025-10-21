using UnityEngine;

namespace Bipolar.FSM
{
    [CreateAssetMenu]
    public class FSMAsset : ScriptableObject
    {
        [SerializeField]
        private int stateCount;
    }
}
