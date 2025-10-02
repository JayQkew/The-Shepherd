using System;
using UnityEngine;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenIdle : DuckenBaseState
    {
        public override void EnterState(DuckenStateManager manager) {
            Debug.Log("Enter -- DuckenIdle");
        }

        public override void UpdateState(DuckenStateManager manager) {
            Debug.Log("Update -- DuckenIdle");
        }

        public override void ExitState(DuckenStateManager manager) {
            Debug.Log("Exit -- DuckenIdle");
        }
    }
}
