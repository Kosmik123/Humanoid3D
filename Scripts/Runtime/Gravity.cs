using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public interface IGravity
    { }
    
    public interface IGravity<in THumanoid> : IGravity
        where THumanoid : IHumanoid
    { 
        void ApplyGravity(THumanoid humanoid);
    }

    public interface IDynamicGravity : IGravity<IHumanoid<Rigidbody>>
    { }

    public interface IKinematicGravity : IGravity<IHumanoid<CharacterController>>
    { }

    public interface IAnyGravity : IGravity<IHumanoid<Component>>
    { }
}
