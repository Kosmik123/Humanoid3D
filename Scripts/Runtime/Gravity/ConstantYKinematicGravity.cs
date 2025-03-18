using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public abstract class ConstantYGravity : ScriptableObject
    {
        [SerializeField]
        protected float constantY = 0f;
    }

    public class ConstantYKinematicGravity : ConstantYGravity, IKinematicGravity
    {
        public void ApplyGravity(Humanoid<CharacterController> humanoid)
        {
            var position = humanoid.Transform.position;
            position.y = constantY;
            humanoid.Transform.position = position;
        }
    }
}
