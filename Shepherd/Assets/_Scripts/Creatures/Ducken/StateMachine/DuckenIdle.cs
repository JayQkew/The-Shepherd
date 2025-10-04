using System;
using UnityEngine;
using Utilities;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenIdle : DuckenBaseState
    {
        public Timer idleTimer;
        public override void EnterState(DuckenManager manager) {
            manager.boid.activeBoids = false;
            idleTimer.SetMaxTime(manager.stats.idleTime.RandomValue());
        }

        public override void UpdateState(DuckenManager manager) {
            idleTimer.Update();
            if (idleTimer.IsFinished) {
                manager.SwitchRandomState();
            }
        }

        public override void ExitState(DuckenManager manager) {
        }
    }
}
