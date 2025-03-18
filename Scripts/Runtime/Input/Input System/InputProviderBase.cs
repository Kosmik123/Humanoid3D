#if ENABLE_INPUT_SYSTEM
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bipolar.Input.InputSystem
{
	public abstract class InputProviderBase : MonoBehaviour
    {
		[SerializeField]
		private InputActionReference inputAction;

        protected InputAction inputActionInstance;

        protected virtual void OnEnable()
        {
            inputActionInstance = inputAction.action.Clone();
            inputAction.action.Enable();
        }

        protected virtual void OnDisable()
        {
			inputActionInstance.Disable();
			inputActionInstance = null;
		}
	}
}
#endif
