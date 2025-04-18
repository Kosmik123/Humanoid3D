using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
	public class HumanoidUpdater : MonoBehaviour
    {
        [SerializeField]
        private Serialized<IHumanoidComponent<Humanoid>>[] components;
		public IReadOnlyList<IHumanoidComponent<Humanoid>> Components => (IReadOnlyList<IHumanoidComponent<Humanoid>>)components;
    }
}
