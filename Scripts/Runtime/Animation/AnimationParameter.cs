using UnityEngine;

namespace Bipolar.Humanoid3D.Animation
{
    [System.Serializable]
    public struct AnimationParameter
    {
        [SerializeField]
        private string name;
        [SerializeField, HideInInspector]
        private int hash;
        private bool hasValue;

        public int Value
        {
            get
            {
                if (hasValue == false)
                {
                    hash = Animator.StringToHash(name);
                    hasValue = true;
                }
                return hash;
            }
        }

        public AnimationParameter(string name)
        {
            this.name = name;
            hash = Animator.StringToHash(name);
            hasValue = true;
        }

        public static implicit operator int(AnimationParameter parameter) => parameter.Value;
        public static explicit operator AnimationParameter(string parameterName) => new AnimationParameter(parameterName);
    }
}
