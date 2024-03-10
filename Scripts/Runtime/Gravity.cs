using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public interface IGravity
    { }

    public interface IGravity<TBody> : IGravity
        where TBody : Component
    {
        void ApplyGravity(Humanoid<TBody> humanoid);
    }

    public interface IDynamicGravity : IGravity<Rigidbody>
    {

    }

    public interface IKinematicGravity : IGravity<CharacterController>
    {

    }
}
