using System;
using UnityEngine;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenEat : DuckenBaseState
    {
        public override void EnterState(DuckenStateManager manager) {
            Debug.Log("Enter -- DuckenEat");
        }

        public override void UpdateState(DuckenStateManager manager) {
            Debug.Log("Update -- DuckenEat");
        }

        public override void ExitState(DuckenStateManager manager) {
            Debug.Log("Exit -- DuckenEat");
        }
    }
}
