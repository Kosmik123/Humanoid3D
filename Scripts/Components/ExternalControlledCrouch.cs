namespace Bipolar.Humanoid3D.Components
{
    public class ExternalControlledCrouch : CrouchBase
    {
        public void SetCrouching(bool crouching)
        {
            IsCrouching = crouching;
        }
    }
}
