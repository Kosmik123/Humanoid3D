using Bipolar.Input;
using System.Collections.Generic;
using UnityEngine;
using Bipolar.Core;
#if NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif

namespace Bipolar.Humanoid3D
{
    public class DefaultHumanoidMovement : HumanoidMovement
    {
        [SerializeField, RequireInterface(typeof(IMoveInputProvider))]
        private Object moveInputProvider;
        public IMoveInputProvider MoveInputProvider
        {
            get => moveInputProvider as IMoveInputProvider;
            set => moveInputProvider = (Object)value;
        }

        [SerializeField]
        private Object[] speedModifiers;
        private List<ISpeedModifier> speedModifiersList;
        protected override IReadOnlyList<ISpeedModifier> SpeedModifiers => speedModifiersList;

        [SerializeField, Range(0, 1)]
        private float sideModifier = 1;
        [SerializeField, Range(0, 1)]
        private float backModifier = 1;

        private Vector3 velocity;
        public override Vector3 Velocity => velocity;

#if NAUGHTY_ATTRIBUTES
        [NaughtyAttributes.ShowNonSerializedField]
#endif
        private float currentSpeed;

        private void Awake()
        {
            speedModifiersList = new List<ISpeedModifier>(speedModifiers.Length);
            for (int i = 0; i < speedModifiers.Length; i++)
                if (speedModifiers[i] is ISpeedModifier speedModifier)
                    speedModifiersList.Add(speedModifier);
        }

        internal override void CalculateVelocity()
        {
            currentSpeed = GetSpeed();

            Vector3 velocity = currentSpeed * GetMovementDirection();
            if (velocity.sqrMagnitude > currentSpeed * currentSpeed)
                velocity = velocity.normalized * currentSpeed;

            this.velocity = velocity;
        }

        private Vector3 GetMovementDirection()
        {
            var moveInput = MoveInputProvider.GetMovement();
            float x = moveInput.x * sideModifier;
            float z = moveInput.y;
            if (z < 0)
                z *= backModifier;

            return transform.forward * z + transform.right * x;
        }

        private void OnValidate()
        {
            Extensions.ValidateInterfacesArray<ISpeedModifier>(ref speedModifiers);
            MoveInputProvider = MoveInputProvider;
        }
    }

    public static class Extensions
    {
        public static void ValidateInterfacesArray<T>(ref Object[] array)
        {
            if (array == null)
                return;
            var valid = new List<Object>(array.Length);
            foreach (var element in array)
                if (element is T)
                    valid.Add(element);
            array = valid.ToArray();
        }
    }
}


