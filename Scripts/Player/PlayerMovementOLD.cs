using System;
using UnityEngine;

namespace Bipolar.Humanoid3D.Player
{
    [Obsolete]
    public class PlayerMovementOLD : MonoBehaviour
    {
        [SerializeField]
        private CharacterControllerHumanoid humanoid;

        [System.Serializable]
        public class RunProperties
        {
            [SerializeField]
            private KeyCode key = KeyCode.LeftShift;
            public KeyCode Key => key;
            [SerializeField]
            private float speedModifier = 1.5f;
            public float SpeedModifier => speedModifier;
        }

        [Header("Settings")]
        [SerializeField]
        private RunProperties runProperties;

        [Range(0f, 1f)]
        [SerializeField]
        private float sideWalkModifier = 1;

        [SerializeField]
        private bool smoothInput;

        [Header("States")]
        [SerializeField]
        private bool isRunning;
        public float horizontal, vertical;

        void Update()
        {
            GetInputs();
            HandleMove();
        }

        private void GetInputs()
        {
            horizontal = UnityEngine.Input.GetAxisRaw("Horizontal") * (smoothInput ? Mathf.Abs(UnityEngine.Input.GetAxis("Horizontal")) : 1);
            vertical = UnityEngine.Input.GetAxisRaw("Vertical") * (smoothInput ? Mathf.Abs(UnityEngine.Input.GetAxis("Vertical")) : 1);
            isRunning = UnityEngine.Input.GetKey(runProperties.Key);
        }

        private void HandleMove()
        {
            Vector3 moveDirection =
                (vertical * transform.forward +
                horizontal * transform.right).normalized;

            float speed = isRunning ? runProperties.SpeedModifier : 1;
            humanoid.AddVelocity(moveDirection * speed);
        }

        private void OnDisable()
        {
            horizontal = 0;
            vertical = 0;
            isRunning = false;
        }
    }
}