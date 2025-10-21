#if ENABLE_INPUT_SYSTEM
using Bipolar.Humanoid3D;
using UnityEngine;

namespace Bipolar.Humanoid3D.InputSystem
{
    [AddComponentMenu(Paths.Input + "Input System Axis Input Provider")]
	public class AxisInputProvider : InputProviderBase, IAxisInputProvider
    {
        private enum AxisType
        {
            FloatValue,
            AxisX,
            AxisY,
        }
        
        [SerializeField]
        private AxisType axis;

		public float GetAxis() => axis switch
		{
			AxisType.AxisX => inputActionInstance.ReadValue<Vector2>().x,
			AxisType.AxisY => inputActionInstance.ReadValue<Vector2>().y,
			_ => inputActionInstance.ReadValue<float>(),
		};
	}
}
#endif
