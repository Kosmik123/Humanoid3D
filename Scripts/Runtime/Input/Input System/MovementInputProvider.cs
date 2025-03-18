#if ENABLE_INPUT_SYSTEM
using UnityEngine;

namespace Bipolar.Input.InputSystem
{
    [AddComponentMenu(Paths.Components + "Input System Movement Input Provider")]
    public class MovementInputProvider : InputProviderBase, IMoveInputProvider
    {
		public Vector2 GetMovement() => inputActionInstance.ReadValue<Vector2>();
	}
}
#endif
