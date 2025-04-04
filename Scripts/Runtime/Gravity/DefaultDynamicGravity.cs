﻿using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public class DefaultDynamicGravity : DefaultGravity, IPhysicalGravity
    {
        public void ApplyGravity(Humanoid<Rigidbody> humanoid)
        {
            humanoid.Body.useGravity = false;
            float scale = GetScale(humanoid.Velocity.y);
            var gravity = Physics.gravity * scale;
            humanoid.Body.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}
