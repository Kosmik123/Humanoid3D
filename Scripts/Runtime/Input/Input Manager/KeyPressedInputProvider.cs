using UnityEngine;

namespace Bipolar.Humanoid3D.InputManager
{
    [AddComponentMenu(Paths.Input + "Input Manager Key Press Input Provider")]
    public class KeyPressedInputProvider : MonoBehaviour, IBoolInputProvider
    {
        [SerializeField]
        private KeyCode[] keys;

        public bool IsActive()
        {
            for (int i = 0; i < keys.Length; i++)
                if (UnityEngine.Input.GetKey(keys[i]))
                    return true;
            
            return false;
        }
    }
}
