using System;
using UnityEngine;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenSleep : DuckenBaseState
    {
        public override void EnterState(DuckenStateManager manager) {
            Debug.Log("Enter -- DuckenSleep");
        }

        public override void UpdateState(DuckenStateManager manager) {
            Debug.Log("Update -- DuckenSleep");
        }

        public override void ExitState(DuckenStateManager manager) {
            Debug.Log("Exit -- DuckenSleep");
        }
    }
}
