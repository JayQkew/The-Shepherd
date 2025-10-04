using Boids;
using TimeSystem;
using UnityEngine;

namespace Creatures.Ducken
{
    public class DuckenStateManager : MonoBehaviour
    {
        public DuckenBaseState currState;
        [HideInInspector] public Ducken ducken;
        [HideInInspector] public Boid boid;
        public DuckenStats stats;

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

        private void Start() {
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
    }

    public abstract class DuckenBaseState
    {
        public abstract void EnterState(DuckenStateManager manager);
        public abstract void UpdateState(DuckenStateManager manager);
        public abstract void ExitState(DuckenStateManager manager);
    }
}