using Bipolar;
using Bipolar.Humanoid3D;
using UnityEngine;

public class MovementDirectionRotation : MonoBehaviour
{
    [SerializeField]
    private Humanoid humanoid;

    [SerializeField]
    private float rotationSpeed = 200;

    private Angle rotationAngle;
    private float targetRotationAngle;

    private void OnEnable()
    {
        rotationAngle = Angle.FromDegrees(humanoid.transform.rotation.eulerAngles.y);
    }

    private void Update()
    {
        if (humanoid.IsMoving == false)
            return;

        var humanoidTransform = humanoid.transform;
        var velocity = humanoid.Velocity;
        velocity.y = 0;
        targetRotationAngle = Vector3.SignedAngle(Vector3.forward, velocity, Vector3.up);

        rotationAngle = Angle.FromDegrees(Mathf.MoveTowardsAngle(rotationAngle.ToDegrees(), targetRotationAngle, rotationSpeed * Time.deltaTime));
        humanoidTransform.rotation = Quaternion.AngleAxis(rotationAngle.ToDegrees(), Vector3.up);
    }
}
