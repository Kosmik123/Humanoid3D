using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public interface IGravity<in THumanoid>
        where THumanoid : Humanoid
    { 
        void ApplyGravity(THumanoid humanoid);
    }

    public interface IDynamicGravity : IGravity<Humanoid<Rigidbody>>
    { }

    public interface IKinematicGravity : IGravity<Humanoid<CharacterController>>
    { }

    public interface IGravity : IGravity<Humanoid>
    { }
}
