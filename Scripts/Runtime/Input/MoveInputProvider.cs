using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public interface IMoveInputProvider
    {
        Vector2 GetMovement();
    }

    [System.Serializable]
    public class MoveInputProvider : Serialized<IMoveInputProvider>, IMoveInputProvider
    {
        public Vector2 GetMovement() => Value.GetMovement();
    }

    public interface IBoolInputProvider
    {
        bool IsActive();
    }

    [System.Serializable]
    public class BoolInputProvider : Serialized<IBoolInputProvider>, IBoolInputProvider
    {
        public bool IsActive() => Value.IsActive();
    }
}
