using System;
using UnityEngine.Serialization;
using Utilities;

namespace Creatures.Sheep
{
    [Serializable]

    public class SheepPanic : SheepBaseState
    {
        public Timer panicTimer;
        public override void EnterState(SheepManager manager) {
            manager.gui.PlayAnim("Idle");
            manager.boid.activeBoids = true;
            panicTimer.SetMaxTime(manager.sheepData.panicTime.RandomValue());
        }

        public override void UpdateState(SheepManager manager) {
            panicTimer.Update();
            manager.gui.UpdateSuppAnims();
            if (panicTimer.IsFinished) {
                manager.SwitchState(manager.sheepChill);
            }
        }

        public override void ExitState(SheepManager manager) {
            manager.boid.activeBoids = false;
        }
    }
}
