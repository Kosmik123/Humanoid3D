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

    public class HumanoidController : MonoBehaviour
    {
        [SerializeField]
        private Humanoid humanoid;
        [SerializeField]
        private HumanoidMovement movement;

        [SerializeField]
        private HumanoidComponent[] components;
        public IReadOnlyList<HumanoidComponent> Components => components;

        private bool firstFixedUpdate;

        private void Awake()
        {
            if (components == null || components.Length == 0)
                components = GetComponents<HumanoidComponent>();
            foreach (var component in components)
                component.Init(humanoid);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            humanoid.ApplyGravity(deltaTime);
            movement.CalculateMotion(deltaTime);

            humanoid.AddMovement(movement.Motion);

            foreach (var component in components)
                if (component.enabled)
                    component.DoUpdate(deltaTime);

            humanoid.ApplyMovement(deltaTime);
        }

        private void FixedUpdate()
        {
            if (firstFixedUpdate)
                FirstFixedUpdate();
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
            if (Application.isPlaying)
                foreach (var component in components)
                    if (component != null)
                        component.Init(humanoid);
        }
    }
}