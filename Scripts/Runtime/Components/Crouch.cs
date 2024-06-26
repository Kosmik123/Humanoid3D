﻿//using UnityEngine;

//namespace Bipolar.Humanoid3D.Components
//{
//    public abstract class Crouch : HumanoidComponent, ISpeedModifier
//    {
//        [Header("Settings")]
//        [SerializeField]
//        private Transform head;

//        [field: SerializeField]
//        public float TransitionTime { get; set; } = 0.3f;

//        [field: SerializeField]
//        public float NormalHeight { get; set; } = 1.8f;

//        [field: SerializeField]
//        public float CrouchHeigth { get; set; } = 1;

//        [field: SerializeField]
//        public float SpeedModifier { get; set; } = 0.7f;

//        [field: Header("States"), SerializeField]
//        public bool IsCrouching { get; protected set; }

//        [field: SerializeField]
//        public Vector3 IdleHeadPosition { get; set; }

//        private float timer;

//#if NAUGHTY_ATTRIBUTES
//        [NaughtyAttributes.ReadOnly]
//#endif
//        [SerializeField]
//        private float crouchingProgress;

//        private void Awake()
//        {
//            IdleHeadPosition = head.localPosition;
//        }

//        public override void DoUpdate(Humanoid humanoid)
//        {
//            crouchingProgress = GetCrouchingProgress(Time.deltaTime);
//            float newHeight = Mathf.Lerp(NormalHeight, CrouchHeigth, crouchingProgress);
//            humanoid.Height = newHeight;
//            humanoid.Center = new Vector3(0, 0.5f * newHeight);
//            var headPosition = IdleHeadPosition;
//            headPosition.y -= (NormalHeight - newHeight);
//            head.localPosition = headPosition;
//        }

//        private float GetCrouchingProgress(float delta)
//        {
//            if (TransitionTime == 0)
//                return IsCrouching ? 1 : 0;

//            if (IsCrouching && timer < TransitionTime)
//            {
//                timer += delta;
//            }
//            else if (IsCrouching == false && timer > 0)
//            {
//                timer -= delta;
//            }

//            return timer / TransitionTime;
//        }

//        public void ModifySpeed(ref float speed)
//        {
//            speed *= Mathf.Lerp(1, SpeedModifier, crouchingProgress);
//        }
//    }
//}
