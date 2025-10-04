using System;
using UnityEngine;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenEat : DuckenBaseState
    {
        public override void EnterState(DuckenManager manager) {
            Debug.Log("Enter -- DuckenEat");
        }

        public override void UpdateState(DuckenManager manager) {
            Debug.Log("Update -- DuckenEat");
        }

        public override void ExitState(DuckenManager manager) {
            Debug.Log("Exit -- DuckenEat");
        }
    }
}
