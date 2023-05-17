using Bipolar.Core.Input;
using System.Collections.Generic;
using UnityEngine;
#if NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif

namespace Bipolar.Humanoid3D
{
    public class DefaultHumanoidMovement : HumanoidMovement
    {
        [SerializeField]
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

        [SerializeField, Range(0,1)]
        private float sideModifier = 1;
        [SerializeField, Range(0,1)]
        private float backModifier = 1;

        private Vector3 movement;
        public override Vector3 Motion => movement;

#if NAUGHTY_ATTRIBUTES
        [SerializeField, ReadOnly]
#endif
        private float currentSpeed;

        private void Awake()
        {
            speedModifiersList = new List<ISpeedModifier>(speedModifiers.Length);
            for (int i = 0; i < speedModifiers.Length; i++)
                if (speedModifiers[i] is ISpeedModifier speedModifier)
                    speedModifiersList.Add(speedModifier);
        }

        internal override void CalculateMotion(float deltaTime)
        {
            currentSpeed = GetSpeed();

            Vector3 velocity = currentSpeed * GetMovementDirection();
            if (velocity.sqrMagnitude > currentSpeed * currentSpeed)
                velocity = velocity.normalized * currentSpeed;

            movement = ForwardProvider.rotation * velocity * deltaTime;
        }

        private Vector3 GetMovementDirection()
        {
            float x = MoveInputProvider.GetHorizontal() * sideModifier;
            float z = MoveInputProvider.GetVertical();
            if (z < 0)
                z *= backModifier;
            
            return new Vector3(x, 0, z);
        }

        private void OnValidate()
        {
            Extensions.ValidateInterfacesArray<ISpeedModifier>(ref speedModifiers);
            MoveInputProvider = MoveInputProvider;
        }
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
