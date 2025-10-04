using System;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;


namespace Creatures.Sheep
{
    [Serializable]
    public class SheepMove : SheepBaseState
    {
        public Vector3 dir;
        public float speed;
        private Rigidbody rb;
        public Timer moveTimer;
    
        public override void EnterState(SheepManager manager) {
            manager.gui.PlayAnim("Idle");
            manager.boid.activeBoids = true;
            if (!rb) rb = manager.rb;
        
            dir = Random.insideUnitCircle.normalized;
            speed = manager.stats.walkSpeed.RandomValue();
        
            moveTimer.SetMaxTime(manager.stats.walkTime.RandomValue());
        }

        public override void UpdateState(SheepManager manager) {
            moveTimer.Update();
            manager.gui.UpdateSuppAnims();
            Vector3 moveForce = new Vector3(dir.x, 0, dir.z) * speed;
            if (moveTimer.IsFinished) {
                manager.SwitchState(manager.GetRandomState());
                return;
            }
            rb.AddForce(moveForce, ForceMode.Acceleration);
        }

        public override void ExitState(SheepManager manager) {
        }
    }
}
