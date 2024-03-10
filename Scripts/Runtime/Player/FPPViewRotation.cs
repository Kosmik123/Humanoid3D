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
        public Vector2 sensitivity = 1.8f * Vector2.one;

        [SerializeField]
        private float minPitchAngle = -90;
        [SerializeField]
        private float maxPitchAngle = 90;

        [Header("States")]
        [SerializeField]
        private float headPitchAngle = 0f;

        private void Reset()
        {
            TryAssignBody();
            TryAssignHead();

            void TryAssignBody()
            {
                Component body = GetComponentInParent<Rigidbody>();
                if (body)
                {
                    this.body = body.transform;
                    return;
                }

                body = GetComponentInParent<CharacterController>();
                if (body)
                {
                    this.body = body.transform;
                    return;
                }
            }
            void TryAssignHead()
            {
                if (body != transform)
                {
                    head = transform;
                    return;
                }

                head = transform.Find("Head");
                if (head == null)
                    head = transform.Find("Camera");
            }
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            Vector2 moveInput = InputProvider.GetMotion();
            moveInput.Scale(sensitivity);
            
            headPitchAngle = Mathf.Clamp(headPitchAngle - moveInput.y, minPitchAngle, maxPitchAngle);

            head.transform.localRotation = Quaternion.AngleAxis(headPitchAngle, Vector3.right);
            body.Rotate(Vector3.up * moveInput.x);
        }

        private void OnValidate()
        {
            InputProvider = InputProvider;
        }
    }
}
