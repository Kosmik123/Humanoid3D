namespace Bipolar.Humanoid3D
{
    public interface IHumanoidComponent<in THumanoid> 
        where THumanoid : Humanoid
    {
        void Apply(THumanoid humanoid);
    }
}
