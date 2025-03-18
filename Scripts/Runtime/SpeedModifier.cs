namespace Bipolar.Humanoid3D
{
	public interface ISpeedModifier
	{
		void ModifySpeed(ref float speed);
	}

	public class SpeedModifier : Serialized<ISpeedModifier>, ISpeedModifier
	{
		public void ModifySpeed(ref float speed) => ModifySpeed(ref speed);
	}
}
