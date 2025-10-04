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

        public DuckenBaseState GetRandomState() {
            DuckenBaseState[] dayStates =
            {
                duckenIdle,
                duckenIdle,
                duckenEat,
                duckenSleep,
                duckenMove
            };

            DuckenBaseState[] nightStates = { duckenSleep };
            
            DuckenBaseState[] states = TimeManager.Instance.currPhase == DayPhaseName.Night ? nightStates : dayStates;
            
            return states[Random.Range(0, states.Length)];
        }

        public override void BarkedAt(Vector3 sourcePosition) {
            base.BarkedAt(sourcePosition);
            switch (currForm) {
                case Form.Ducken:
                    //follow the source
                    break;
                case Form.Chicken:
                    //jump and run around frantically
                    break;
                case Form.Duck:
                    // freeze and turn into ice (slippery)
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public abstract class DuckenBaseState
    {
        public abstract void EnterState(DuckenManager manager);
        public abstract void UpdateState(DuckenManager manager);
        public abstract void ExitState(DuckenManager manager);
    }
}