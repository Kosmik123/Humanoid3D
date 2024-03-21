using UnityEngine;

namespace Bipolar.Humanoid3D.Animation
{
    public abstract class HumanoidAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        protected const string AnimatorName = nameof(animator);

        protected virtual void Reset()
        {
            animator = GetComponentInChildren<Animator>();
        }

        protected void SetTrigger(int hash)
        {
            if (hash != 0)
                animator.SetTrigger(hash);
        }

        protected void ResetTrigger(int hash)
        {
            if (hash != 0)
                animator.ResetTrigger(hash);
        }

        protected void SetBool(int hash, bool value)
        {
            if (hash != 0)
                animator.SetBool(hash, value);
        }

        protected void SetInteger(int hash, int value)
        {
            if (hash != 0)
                animator.SetInteger(hash, value);
        }

        protected void SetFloat(int hash, float value)
        {
            if (hash != 0)
                animator.SetFloat(hash, value);
        }
    }
}
