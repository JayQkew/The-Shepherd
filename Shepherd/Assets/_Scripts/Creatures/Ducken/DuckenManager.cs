using System;
using TimeSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Creatures.Ducken
{
    public class DuckenManager : Ducken
    {
        private DuckenBaseState currState;

        [Space(25)]
        [Header("State Manager")]
        [Space(10)]
        public DuckenEat duckenEat;
        [Space(10)]
        public DuckenIdle duckenIdle;
        [Space(10)]
        public DuckenSleep duckenSleep;
        [Space(10)]
        public DuckenRun duckenRun;
        [Space(10)]
        public DuckenMove duckenMove;

        protected override void Start() {
            base.Start();
            currState = duckenIdle;
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

        public void SwitchRandomState() {
            DuckenBaseState[] dayStates =
            {
                duckenIdle,
                duckenEat,
                duckenSleep,
                duckenMove,
                duckenMove,
                duckenMove
            };

            DuckenBaseState[] nightStates = { duckenSleep };
            
            DuckenBaseState[] states = TimeManager.Instance.currPhase == DayPhaseName.Night ? nightStates : dayStates;
            DuckenBaseState state = states[Random.Range(0, states.Length)];
            SwitchState(state);
        }

        public override void BarkedAt(Vector3 sourcePosition) {
            base.BarkedAt(sourcePosition);
            switch (currForm) {
                case Form.Ducken:
                    //follow the source
                    MoveTo(sourcePosition);
                    break;
                case Form.Chicken:
                    //jump and run around frantically
                    rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
                    SwitchState(duckenMove);
                    break;
                case Form.Duck:
                    // freeze and turn into ice (slippery)
                    rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
                    SwitchState(duckenIdle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void MoveTo(Vector3 targetPos) {
            currState.ExitState(this);
            currState = duckenMove.Target(targetPos);
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