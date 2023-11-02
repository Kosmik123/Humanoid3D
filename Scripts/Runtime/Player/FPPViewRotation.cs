using Bipolar.Core;
using Bipolar.Input;
using UnityEngine;

namespace Bipolar.Humanoid3D.Player
{
    public class FPPViewRotation : MonoBehaviour
    {
        [Header("To Link")]
        [SerializeField]
        private Transform head;
        [SerializeField]
        private Transform body;
        [SerializeField, RequireInterface(typeof(IMoveInputProvider))]
        private Object movementInputProvider;
        public IMoveInputProvider InputProvider
        {
            get => movementInputProvider as IMoveInputProvider;
            set
            {
                movementInputProvider = (Object)value;
            }
        }

        [Header("Properties")]
        [SerializeField]
        public Vector2 sensitivity = Vector2.one;

        [SerializeField]
        private float minCameraAngle = -90;
        [SerializeField]
        private float maxCameraAngle = 90;

        [Header("States")]
        [SerializeField]
        private float xAngle = 0f;

        private void Start()
        {
            Cursor.visible = false;
        }

        void Update()
        {
            Vector2 moveInput = InputProvider.GetMotion();
            moveInput.Scale(sensitivity);
            
            xAngle = Mathf.Clamp(xAngle - moveInput.y, minCameraAngle, maxCameraAngle);

            head.transform.localRotation = Quaternion.AngleAxis(xAngle, Vector3.right);
            body.Rotate(Vector3.up * moveInput.x);
        }

        private void OnValidate()
        {
            InputProvider = InputProvider;
        }
    }
}
