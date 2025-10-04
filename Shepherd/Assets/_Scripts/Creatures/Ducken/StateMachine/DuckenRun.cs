using System;
using UnityEngine;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenRun : DuckenBaseState
    {
        public override void EnterState(DuckenManager manager) {
            Debug.Log("Enter -- DuckenRun");
        }

        public override void UpdateState(DuckenManager manager) {
            Debug.Log("Update -- DuckenRun");
        }

        public override void ExitState(DuckenManager manager) {
            Debug.Log("Exit -- DuckenRun");
        }
    }
}
