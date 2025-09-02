using System;

namespace _Scripts.Creatures.Sheep.StateMachine
{
    [Serializable]
    public class SheepIdle : SheepBaseState
    {
        public Timer idleTimer;
        public override void EnterState(SheepStateManager manager) {
            manager.gui.PlayAnim("Idle");
            manager.boids.activeBoids = false;
            idleTimer.SetMaxTime(manager.stats.idleTime.RandomValue());
        }

        public override void UpdateState(SheepStateManager manager) {
            idleTimer.Update();
            manager.gui.UpdateSuppAnims();
            if (idleTimer.IsFinished) {
                manager.SwitchState(manager.GetRandomState());
            }
        }

        public override void ExitState(SheepStateManager manager) {
        }
    }
}
