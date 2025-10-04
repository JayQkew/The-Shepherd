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
        public Timer moveTimer;

        private bool hasTarget;
        private Vector3 targetPos;
        private Rigidbody rb;


        public override void EnterState(SheepManager manager) {
            manager.gui.PlayAnim("Idle");
            manager.boid.activeBoids = true;
            if (!rb) rb = manager.rb;

            if (!hasTarget) {
                dir = Random.insideUnitCircle.normalized;
                speed = manager.stats.walkSpeed.RandomValue();
            }

            moveTimer.SetMaxTime(manager.stats.walkTime.RandomValue());
        }

        public override void UpdateState(SheepManager manager) {
            manager.gui.UpdateSuppAnims();
            moveTimer.Update();
            
            if (moveTimer.IsFinished) {
                manager.SwitchState(manager.GetRandomState());
                return;
            }
            
            if (!hasTarget) {
                Vector3 moveForce = new Vector3(dir.x, 0, dir.z) * speed;
                rb.AddForce(moveForce, ForceMode.Acceleration);
            }
            else {
                dir = (targetPos - rb.position).normalized;
                Vector3 move = dir * (speed * Time.deltaTime);
                rb.MovePosition(rb.position + move);

                float distance = Vector3.Distance(rb.position, targetPos);
                if (distance < 0.5f) {
                    hasTarget = false;
                    manager.SwitchState(manager.GetRandomState());
                }
            }
        }

        public override void ExitState(SheepManager manager) {
            hasTarget = false;
        }

        public SheepMove Target(Vector3 target) {
            hasTarget = true;
            targetPos = target;
            return this;
        }
    }
}