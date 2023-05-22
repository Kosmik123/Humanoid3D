using UnityEngine;

namespace Bipolar.Humanoid3D.Animation
{
    public abstract class HumanoidAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        protected virtual void Reset()
        {
            animator = GetComponentInChildren<Animator>();
        }

        protected void SetTrigger(int hash) => animator.SetTrigger(hash);
        protected void ResetTrigger(int hash) => animator.ResetTrigger(hash);
        protected void SetBool(int hash, bool value) => animator.SetBool(hash, value);
        protected void SetInteger(int hash, int value) => animator.SetInteger(hash, value);
        protected void SetFloat(int hash, float value) => animator.SetFloat(hash, value);
    }
}
