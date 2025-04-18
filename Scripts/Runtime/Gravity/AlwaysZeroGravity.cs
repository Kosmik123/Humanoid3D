using UnityEngine;

namespace Bipolar.Humanoid3D
{
	[CreateAssetMenu]
	public class AlwaysZeroGravity : DefaultGravity, IGravity
	{
		public void ApplyGravity(Humanoid humanoid)
		{
			var position = humanoid.Transform.position;
			position.y = 0;
			humanoid.Transform.position = position;
		}
	}
}
