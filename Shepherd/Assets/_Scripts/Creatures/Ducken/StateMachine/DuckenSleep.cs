using System;
using UnityEngine;
using Utilities;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenSleep : DuckenBaseState
    {
        public Timer sleepTimer;
        public override void EnterState(DuckenManager manager) {
            manager.boid.activeBoids = false;
            sleepTimer.SetMaxTime(manager.duckenData.sleepTime.RandomValue());
        }

        public override void UpdateState(DuckenManager manager) {
            sleepTimer.Update();
            if (sleepTimer.IsFinished) {
                manager.SwitchRandomState();
            }
        }

        public override void ExitState(DuckenManager manager) {
        }
    }
}
