using System;
using UnityEngine;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenMove : DuckenBaseState
    {
        public override void EnterState(DuckenStateManager manager) {
            Debug.Log("Enter -- DuckenMove");
        }

        public override void UpdateState(DuckenStateManager manager) {
            Debug.Log("Update -- DuckenMove");
        }

        public override void ExitState(DuckenStateManager manager) {
            Debug.Log("Exit -- DuckenMove");
        }
    }
}
