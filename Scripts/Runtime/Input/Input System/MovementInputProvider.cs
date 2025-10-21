#if ENABLE_INPUT_SYSTEM
using UnityEngine;

namespace Bipolar.Humanoid3D.InputSystem
{
    [AddComponentMenu(Paths.Input + "Input System Movement Input Provider")]
    public class MovementInputProvider : InputProviderBase, IMoveInputProvider
    {
		public Vector2 GetMovement() => inputActionInstance.ReadValue<Vector2>();
	}
}
#endif
