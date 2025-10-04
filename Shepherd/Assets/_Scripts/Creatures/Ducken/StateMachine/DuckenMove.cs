using System;
using UnityEngine;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenMove : DuckenBaseState
    {
        public override void EnterState(DuckenManager manager) {
            Debug.Log("Enter -- DuckenMove");
        }

        public override void UpdateState(DuckenManager manager) {
            Debug.Log("Update -- DuckenMove");
        }

        public override void ExitState(DuckenManager manager) {
            Debug.Log("Exit -- DuckenMove");
        }
    }
}
