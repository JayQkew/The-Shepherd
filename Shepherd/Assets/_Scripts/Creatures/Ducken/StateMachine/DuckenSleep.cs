using System;
using UnityEngine;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenSleep : DuckenBaseState
    {
        public override void EnterState(DuckenManager manager) {
            Debug.Log("Enter -- DuckenSleep");
        }

        public override void UpdateState(DuckenManager manager) {
            Debug.Log("Update -- DuckenSleep");
        }

        public override void ExitState(DuckenManager manager) {
            Debug.Log("Exit -- DuckenSleep");
        }
    }
}
