using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
    [System.Flags]
    public enum Collision
    {
        None = 0,
        Sides = 1 << 0,
        Above = 1 << 1,
        Below = 1 << 2,
    }

    public class HumanoidUpdater : MonoBehaviour
    {
        [SerializeField]
        private HumanoidComponent[] components;
        public IReadOnlyList<HumanoidComponent> Components => components;
    }

    [RequireComponent(typeof(Humanoid))]
    public class HumanoidController : MonoBehaviour
    {
        private Humanoid _humanoid;
        public Humanoid Humanoid
        {
            get
            {
                if (_humanoid == null)
                    _humanoid = GetComponent<Humanoid>();
                return _humanoid;
            }
        }

        [SerializeField]
        private HumanoidMovement movement;

        private bool firstFixedUpdate;

        private void Update()
        {
            //float deltaTime = Time.deltaTime;
            //movement.CalculateVelocity();
            //_humanoid.AddMovementVelocity(movement.Velocity);
            //foreach (var component in humanoid.Components)
            //    if (component.enabled)
            //        component.DoUpdate(deltaTime);

            //_humanoid.ApplyMovement(deltaTime);
        }

        private void FixedUpdate()
        {
            if (firstFixedUpdate)
                FirstFixedUpdate();
            _humanoid.ApplyGravity();
        }

        private void FirstFixedUpdate()
        {
            firstFixedUpdate = false;
        }

        private void LateUpdate()
        {
            firstFixedUpdate = true;
        }

        private void OnValidate()
        {
            if (_humanoid == null)
                _humanoid = GetComponent<Humanoid>();

            //if (Application.isPlaying)
            //    foreach (var component in components)
            //        if (component != null)
            //            component.Init(_humanoid);
        }
    }
}
