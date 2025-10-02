using UnityEngine;

namespace Creatures.Ducken
{
    public class DuckenStateManager : MonoBehaviour
    {
        public DuckenBaseState currState;

        public DuckenEat duckenEat = new DuckenEat();
        public DuckenIdle duckenIdle = new DuckenIdle();
        public DuckenSleep duckenSleep = new DuckenSleep();
        public DuckenRun duckenRun = new DuckenRun();
        public DuckenMove duckenMove = new DuckenMove();

        private void Start() {
            currState = duckenEat;
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
    }

    public abstract class DuckenBaseState
    {
        public abstract void EnterState(DuckenStateManager manager);
        public abstract void UpdateState(DuckenStateManager manager);
        public abstract void ExitState(DuckenStateManager manager);
    }
}