using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Creatures.Ducken
{
    [Serializable]
    public class DuckenPanic : DuckenBaseState
    {
        public Timer timer;
        public override void EnterState(DuckenManager manager) {
            manager.boid.activeBoids = true;
            timer.SetMaxTime(manager.duckenData.runTime.RandomValue());
        }

        public override void UpdateState(DuckenManager manager) {
            timer.Update();
            if (timer.IsFinished) {
                manager.SwitchState(manager.duckenChill);
            }
        }

        public override void ExitState(DuckenManager manager) {
        }
    }
}
