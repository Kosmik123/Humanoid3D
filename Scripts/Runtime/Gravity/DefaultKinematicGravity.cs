using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public class DefaultKinematicGravity : DefaultGravity, IKinematicGravity
    {
        public void ApplyGravity(IHumanoid<CharacterController> humanoid)
        {
            float scale = GetScale(humanoid.Velocity.y);
            var gravity = Physics.gravity * scale;
            humanoid.Body.Move(gravity * Time.deltaTime); // ITS WRONG! HOWEVER IT WILL BE FIXED LATER
        }
    }
}
