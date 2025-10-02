using System;
using UnityEngine;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenRun : DuckenBaseState
    {
        public override void EnterState(DuckenStateManager manager) {
            Debug.Log("Enter -- DuckenRun");
        }

        public override void UpdateState(DuckenStateManager manager) {
            Debug.Log("Update -- DuckenRun");
        }

        public override void ExitState(DuckenStateManager manager) {
            Debug.Log("Exit -- DuckenRun");
        }
    }
}
