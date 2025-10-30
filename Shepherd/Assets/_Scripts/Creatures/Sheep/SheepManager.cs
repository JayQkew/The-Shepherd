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
        public SheepChill sheepChill;
        [Space(10)]
        public SheepPanic sheepPanic;

        protected override void Start() {
            base.Start();
            currState = sheepChill;
            currState.EnterState(this);
        }

        protected override void Update() {
            base.Update();
            currState.UpdateState(this);
        }

        public void SwitchState(SheepBaseState newState) {
            currState.ExitState(this);
            currState = newState;
            currState.EnterState(this);
        }

        // public void MoveTo(Vector3 pos) {
        //     currState.ExitState(this);
        //     currState = sheepMove.Target(pos);
        //     currState.EnterState(this);
        // }
        //
        // public SheepBaseState GetRandomState() {
        //     SheepBaseState[] dayStates =
        //     {
        //         sheepIdle,
        //         sheepIdle,
        //         sheepEat,
        //         sheepSleep,
        //         sheepMove
        //     };
        //
        //     SheepBaseState[] nightState = { sheepSleep };
        //
        //     SheepBaseState[] states = TimeManager.Instance.currPhase == DayPhaseName.Night ? nightState : dayStates;
        //
        //     return states[Random.Range(0, states.Length)];
        // }

        public override void BarkedAt(Vector3 sourcePositions) {
            base.BarkedAt(sourcePositions);
            SwitchState(sheepPanic);
        }
    }

    public abstract class SheepBaseState
    {
        public abstract void EnterState(SheepManager manager);
        public abstract void UpdateState(SheepManager manager);
        public abstract void ExitState(SheepManager manager);
    }
}