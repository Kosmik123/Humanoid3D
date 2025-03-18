using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public interface IGravity<in THumanoid>
        where THumanoid : Humanoid
    { 
        void ApplyGravity(THumanoid humanoid);
    }

    public interface IPhysicalGravity : IGravity<Humanoid<Rigidbody>>
    { }

    public interface ICharacterGravity : IGravity<Humanoid<CharacterController>>
    { }

    public interface IGravity : IGravity<Humanoid>
    { }
}
