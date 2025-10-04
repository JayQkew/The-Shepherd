using TimeSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Creatures.Sheep
{
    public class SheepManager : Sheep
    {
        private SheepBaseState currState;

        [Space(25)]
        [Header("State Manager")]
        [Space(10)]
        public SheepIdle sheepIdle;
        [Space(10)]
        public SheepEat sheepEat;
        [Space(10)]
        public SheepSleep sheepSleep;
        [Space(10)]
        public SheepMove sheepMove;
        [Space(10)]
        public SheepRun sheepRun;

        protected override void Start() {
            base.Start();
            currState = sheepIdle;
            currState.EnterState(this);
        }

        private void Update() {
            currState.UpdateState(this);
        }

        public void SwitchState(SheepBaseState newState) {
            currState.ExitState(this);
            currState = newState;
            currState.EnterState(this);
        }

        public SheepBaseState GetRandomState() {
            SheepBaseState[] dayStates =
            {
                sheepIdle,
                sheepIdle,
                sheepEat,
                sheepSleep,
                sheepMove
            };

            SheepBaseState[] nightState = { sheepSleep };

            SheepBaseState[] states = TimeManager.Instance.currPhase == DayPhaseName.Night ? nightState : dayStates;

            return states[Random.Range(0, states.Length)];
        }

        public override void BarkedAt(Vector3 sourcePositions) {
            base.BarkedAt(sourcePositions);
            SwitchState(sheepRun);
        }
    }

    public abstract class SheepBaseState
    {
        public abstract void EnterState(SheepManager manager);
        public abstract void UpdateState(SheepManager manager);
        public abstract void ExitState(SheepManager manager);
    }
}