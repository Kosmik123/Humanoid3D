using UnityEngine;

namespace Bipolar.Humanoid3D.Animation
{
    [System.Serializable]
    public struct AnimationParameter
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private int hash;

        public int Value => hash;

        public AnimationParameter(string name)
        {
            this.name = name;
            hash = Animator.StringToHash(name);
        }
    }
}
