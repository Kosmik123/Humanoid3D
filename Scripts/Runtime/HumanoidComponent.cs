using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public interface IHumanoidComponent : IHumanoidComponent<Humanoid>
    {
        void DoUpdate(float deltaTime);
    }

    public interface IHumanoidComponent<in THumanoid>
        where THumanoid : Humanoid
    {
        void Init(THumanoid humanoid);
    }

    public abstract class HumanoidComponent : HumanoidComponent<Humanoid>, IHumanoidComponent
    {
        protected Humanoid humanoid;
        public bool IsInited => humanoid != null;

        public override void Init(Humanoid humanoid)  
        {
            this.humanoid = humanoid;
        }

        protected virtual void OnEnable()
        { }

        public abstract void DoUpdate(float deltaTime);
    }

    public abstract class HumanoidComponent<THumanoid> : MonoBehaviour, IHumanoidComponent<THumanoid>
        where THumanoid : Humanoid
    {
        public abstract void Init(THumanoid humanoid);
    }
}
