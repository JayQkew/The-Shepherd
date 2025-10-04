using System;
using UnityEngine;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenIdle : DuckenBaseState
    {
        public override void EnterState(DuckenManager manager) {
            Debug.Log("Enter -- DuckenIdle");
        }

        public override void UpdateState(DuckenManager manager) {
            Debug.Log("Update -- DuckenIdle");
        }

        public override void ExitState(DuckenManager manager) {
            Debug.Log("Exit -- DuckenIdle");
        }
    }
}
