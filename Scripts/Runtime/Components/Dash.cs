//using UnityEngine;

//namespace Bipolar.Humanoid3D.Components
//{
//    public class Dash : HumanoidComponent
//    {
//        // to będzie musiał być eventowy input provider 
//        [SerializeField]
//        private KeyCode key = KeyCode.F;

//        [SerializeField]
//        private float dashSpeed = 20;
//        public float DashSpeed
//        {
//            get => dashSpeed;
//            set => dashSpeed = value;
//        }

//        [SerializeField]
//        private float dashDuration = 0.3f;
//        public float DashDuration
//        {
//            get => dashDuration;
//            set => dashDuration = value;
//        }

//        [SerializeField]
//        private float cooldownDuration = 0.3f;
//        public float CooldownDuration
//        {
//            get => cooldownDuration;
//            set => cooldownDuration = value;
//        }

//#if NAUGHTY_ATTRIBUTES
//        [NaughtyAttributes.ReadOnly]
//#endif
//        [SerializeField]
//        private float cooldownTimer;

//#if NAUGHTY_ATTRIBUTES
//        [NaughtyAttributes.ReadOnly]
//#endif
//        [SerializeField]
//        private bool cooldownFinished;

//#if NAUGHTY_ATTRIBUTES
//        [NaughtyAttributes.ReadOnly]
//#endif
//        [SerializeField]
//        private bool isDashing;

//        [SerializeField]
//        private Vector3 dashLocalDirection;

//        public override void DoUpdate(Humanoid humanoid)
//        {
//            float dt = Time.deltaTime;
//            if (cooldownFinished == false)
//                cooldownTimer += dt;

//            if (cooldownTimer > cooldownDuration)
//            {
//                cooldownFinished = true;
//                if (UnityEngine.Input.GetKeyDown(key))
//                {
//                    cooldownTimer = 0;
//                    CancelInvoke();
//                    isDashing = true;
//                    Invoke(nameof(StopDashing), dashDuration);
//                }
//            }

//            if (isDashing)
//                humanoid.AddMovementVelocity(dashSpeed * dt * dashLocalDirection);
//        }

//        public void StopDashing()
//        {
//            //humanoid.AddVelocity(dashSpeed * dashLocalDirection);
//            isDashing = false;
//            cooldownFinished = false;
//            dashLocalDirection = Vector3.zero;
//        }

//        public override void Init(Humanoid humanoid)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}
