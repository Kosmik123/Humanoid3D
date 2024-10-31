using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public abstract class DefaultGravity : ScriptableObject
    {
        [field: SerializeField, Tooltip("Gravity scale when going up")]
        public float UpScale { get; set; } = 1;

        [field: SerializeField, Tooltip("Gravity scale when falling down")]
        public float DownScale { get; set; } = 2;

        public float GetScale(float ySpeed)
        {
            return ySpeed < 0 ? DownScale : UpScale;
        }
    }
}
