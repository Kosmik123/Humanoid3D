using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public class KeyInputSpeedModifier : MonoBehaviour, ISpeedModifier
    {
        public KeyCode key = KeyCode.LeftShift;

        [SerializeField]
        private float speedModifier;
        
        public void ModifySpeed(ref float speed)
        {
            if (Input.GetKey(key))
                speed *= speedModifier;
        }
    }
}
