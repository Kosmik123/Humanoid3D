using Bipolar.Core.Input;
using UnityEngine;

namespace Bipolar.Humanoid3D.Player
{
    public class ViewRotation : MonoBehaviour
    {
        [Header("To Link")]
        [SerializeField]
        private Transform head;
        [SerializeField]
        private Transform body;
        [SerializeField]
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
            float mouseX = InputProvider.GetHorizontal() * sensitivity.x;
            float mouseY = InputProvider.GetVertical() * sensitivity.y;

            xAngle = Mathf.Clamp(xAngle - mouseY, minCameraAngle, maxCameraAngle);

            head.transform.localRotation = Quaternion.AngleAxis(xAngle, Vector3.right);
            body.Rotate(Vector3.up * mouseX);
        }

        private void OnValidate()
        {
            InputProvider = InputProvider;
        }
    }
}
