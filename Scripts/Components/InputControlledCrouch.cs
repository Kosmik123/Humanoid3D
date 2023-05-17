using UnityEngine;

namespace Bipolar.Humanoid3D.Components
{
    public class InputControlledCrouch : CrouchBase
    {
        [SerializeField]
        private KeyCode key = KeyCode.LeftControl; // docelowo to ma być jakiś input provider

        public override void DoUpdate(float deltaTime)
        {
            IsCrouching = Input.GetKey(key);
            base.DoUpdate(deltaTime);
        }
    }
}
