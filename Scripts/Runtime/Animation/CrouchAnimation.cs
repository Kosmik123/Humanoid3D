//using UnityEngine;

//namespace Bipolar.Humanoid3D.Animation
//{
//    public class CrouchAnimation : HumanoidAnimation
//    {
//        [SerializeField]
//        private Crouch crouch;

//#if NAUGHTY_ATTRIBUTES
//        [AnimatorParameter(AnimatorName, AnimatorControllerParameterType.Bool)]
//#endif
//        private AnimationParameter crouchParameterName;

//        protected override void Reset()
//        {
//            base.Reset();
//            crouch = GetComponent<Crouch>();
//        }

//        private void Update()
//        {
//            SetBool(crouchParameterName, crouch.IsCrouching);
//        }
//    }
//}
