namespace Bipolar.Humanoid3D
{
	public interface ISpeedModifier
	{
		void ModifySpeed(ref float speed);
	}

	[System.Serializable]	
	public class SpeedModifier : Serialized<ISpeedModifier>, ISpeedModifier
	{
		public void ModifySpeed(ref float speed) => Value.ModifySpeed(ref speed);
	}
}
