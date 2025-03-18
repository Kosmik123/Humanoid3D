﻿namespace Bipolar.Humanoid3D
{
    public interface IAxisInputProvider
    {
        float GetAxis();
    }

	[System.Serializable]
	public class AxisInputProvider : Serialized<IAxisInputProvider>, IAxisInputProvider
	{
		public float GetAxis() => Value.GetAxis();
	}
}
