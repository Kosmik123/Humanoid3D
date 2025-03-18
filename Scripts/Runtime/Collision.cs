namespace Bipolar.Humanoid3D
{
	[System.Flags]
    public enum Collision
    {
        None = 0,
        Sides = 1 << 0,
        Above = 1 << 1,
        Below = 1 << 2,
    }
}
