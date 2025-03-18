using System.Collections.Generic;
using UnityEngine;

namespace Bipolar.Humanoid3D
{
	public class HumanoidUpdater : MonoBehaviour
    {
        [SerializeField]
        private HumanoidComponent[] components;
        public IReadOnlyList<HumanoidComponent> Components => components;
    }
}
