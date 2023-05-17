using UnityEngine;

namespace Bipolar.Humanoid3D
{
    internal interface IHumanoidComponent
    {
        void Init(Humanoid humanoid);
        void DoUpdate(float deltaTime);
    }

    public abstract class HumanoidComponent : MonoBehaviour, IHumanoidComponent
    {
        protected Humanoid humanoid;

        public void Init(Humanoid humanoid)  
        {
            this.humanoid = humanoid;
        }

        protected virtual void OnEnable()
        { }

        public abstract void DoUpdate(float deltaTime);
    }
}
