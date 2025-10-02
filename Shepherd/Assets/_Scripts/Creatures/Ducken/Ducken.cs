using HerdingSystem;
using UnityEngine;

namespace Creatures.Ducken
{
    public class Ducken : HerdAnimal, IBarkable
    {
        private DuckenStateManager duckenStateManager;

        protected override void Awake() {
            base.Awake();
            duckenStateManager = GetComponent<DuckenStateManager>();
        }
        public void BarkedAt(Vector3 sourcePosition) {
            throw new System.NotImplementedException();
        }
    }
}
