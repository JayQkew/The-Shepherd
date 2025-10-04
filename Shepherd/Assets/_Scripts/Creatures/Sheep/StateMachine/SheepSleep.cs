using System;
using Utilities;

namespace Creatures.Sheep
{
    [Serializable]
    public class SheepSleep : SheepBaseState
    {
        public Timer sleepTimer;
        public override void EnterState(SheepManager manager) {
            manager.gui.PlayAnim("Sleep");
            manager.boid.activeBoids = false;
            sleepTimer.SetMaxTime(manager.stats.sleepTime.RandomValue());
        }

        public override void UpdateState(SheepManager manager) {
            sleepTimer.Update();
            if (sleepTimer.IsFinished) {
                manager.SwitchState((manager.GetRandomState()));
            }
        }

        public override void ExitState(SheepManager manager) {
        }
    }
}
