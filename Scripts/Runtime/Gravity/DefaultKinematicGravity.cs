﻿using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public class DefaultKinematicGravity : DefaultGravity, ICharacterGravity
    {
        public void ApplyGravity(Humanoid<CharacterController> humanoid)
        {
            float scale = GetScale(humanoid.Velocity.y);
            var gravity = Physics.gravity * scale;
            humanoid.Body.Move(gravity * Time.deltaTime); // ITS WRONG! HOWEVER IT WILL BE FIXED LATER
        }
    }


}
