using System;
using UnityEngine;
using Utilities;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenRun : DuckenBaseState
    {
        public Timer runTimer;
        public override void EnterState(DuckenManager manager) {
            manager.boid.activeBoids = true;
            runTimer.SetMaxTime(manager.stats.runTime.RandomValue());
        }

        public override void UpdateState(DuckenManager manager) {
            runTimer.Update();
            if (runTimer.IsFinished) {
                manager.SwitchRandomState();
            }
        }

        public override void ExitState(DuckenManager manager) {
        }
    }
}
