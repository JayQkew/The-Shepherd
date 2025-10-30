using System;
using TimeSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Creatures.Ducken
{
    public class DuckenManager : Ducken
    {
        private DuckenBaseState currState;

        [Space(25)]
        [Header("State Manager")]
        [Space(10)]
        public DuckenChill duckenChill;

        [Space(10)]
        public DuckenPanic duckenPanic;

        protected override void Start() {
            base.Start();
            currState = duckenChill;
            currState.EnterState(this);
        }

        private void Update() {
            currState.UpdateState(this);
        }

        public void SwitchState(DuckenBaseState newState) {
            currState.ExitState(this);
            currState = newState;
            currState.EnterState(this);
        }

        public override void BarkedAt(Transform source) {
            base.BarkedAt(source);
            switch (currForm) {
                case Form.Ducken:
                    //follow the source
                    MoveTo(source);
                    break;
                case Form.Chicken:
                    //jump and run around frantically
                    if (IsGrounded()) {
                        Vector3 dir = (transform.position - source.position).normalized;
                        rb.AddForce(dir * duckenData.barkForce, ForceMode.Impulse);
                        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
                    }

                    SwitchState(duckenPanic);
                    break;
                case Form.Duck:
                    // freeze and turn into ice (slippery)
                    if (IsGrounded()) {
                        Vector3 dir = (transform.position - source.position).normalized;
                        rb.AddForce(dir * duckenData.barkForce, ForceMode.Impulse);
                        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
                    }

                    SwitchState(duckenChill);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MoveTo(Transform target) {
            currState.ExitState(this);
            duckenChill.Target(target);
            currState = duckenChill;
            currState.EnterState(this);
        }
    }

    public abstract class DuckenBaseState
    {
        public abstract void EnterState(DuckenManager manager);
        public abstract void UpdateState(DuckenManager manager);
        public abstract void ExitState(DuckenManager manager);
    }
}