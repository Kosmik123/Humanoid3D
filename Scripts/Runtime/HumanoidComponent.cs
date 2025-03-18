using UnityEngine;

namespace Bipolar.Humanoid3D
{
    public interface IHumanoidComponent
    {
        void Apply();
    }

    public interface IHumanoidComponent<in THumanoid> : IHumanoidComponent
        where THumanoid : Humanoid
    { 
        void SetHumanoid(THumanoid humanoid);
    }

    public abstract class BaseHumanoidComponent : MonoBehaviour, IHumanoidComponent
    {
        public abstract void Apply();
    }

    public abstract class HumanoidComponent<TBody> : BaseHumanoidComponent, IHumanoidComponent<Humanoid<TBody>>
        where TBody : Component
    {
        protected Humanoid<TBody> humanoid;
        public void SetHumanoid(Humanoid<TBody> humanoid)
        {
            this.humanoid = humanoid;
        }
    }

    public abstract class HumanoidComponent : HumanoidComponent<Component>
    { }
}
