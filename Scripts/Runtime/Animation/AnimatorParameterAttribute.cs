#if NAUGHTY_ATTRIBUTES
using UnityEngine;

namespace Bipolar.Humanoid3D.Animation
{
    public class AnimatorParameterAttribute : NaughtyAttributes.DrawerAttribute
    {
        public string AnimatorName { get; private set; }
        public AnimatorControllerParameterType? AnimatorParamType { get; private set; } = null;

        public AnimatorParameterAttribute(string animatorName, AnimatorControllerParameterType animatorParamType) : this(animatorName)
        {
            AnimatorParamType = animatorParamType;
        }

        public AnimatorParameterAttribute(string animatorName)
        {
            AnimatorName = animatorName;
        }
    }
}
#endif
