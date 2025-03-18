using System;
using UnityEngine;

namespace Bipolar.Humanoid3D.InputManager
{
	[AddComponentMenu(Paths.Components + "Input Manager Key Press Input Provider")]
	public class KeyPressActionInputProvider : KeyActionInputProvider
    {
		protected override Func<KeyCode, bool> GetCheckingMethod()
		{
			return (key) => UnityEngine.Input.GetKeyDown(key);
		}
	}
}
