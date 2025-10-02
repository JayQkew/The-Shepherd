using TimeSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Creatures.Sheep
{
    public class SheepStateManager : MonoBehaviour
    {
        private SheepBaseState currState;
        [HideInInspector] public Sheep sheep;
        [HideInInspector] public Boids.Boids boids;
        public SheepStats stats;
        public SheepGUI gui;

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

        private void Awake() {
            gui = GetComponent<SheepGUI>();
            sheep = GetComponent<Sheep>();
            boids = GetComponent<Boids.Boids>();
        }

        private void Start() {
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
                sheepIdle,
                sheepEat,
                sheepEat,
                sheepSleep,
                sheepMove
            };

            SheepBaseState[] nightState = { sheepSleep };

            SheepBaseState[] states = TimeManager.Instance.currPhase == DayPhaseName.Night ? nightState : dayStates;

            return states[Random.Range(0, states.Length)];
        }
    }

    public abstract class SheepBaseState
    {
        public abstract void EnterState(SheepStateManager manager);
        public abstract void UpdateState(SheepStateManager manager);
        public abstract void ExitState(SheepStateManager manager);
    }
}