using System;
using UnityEngine;

namespace Creatures
{
    public class BugStateManager : MonoBehaviour
    {
        public BugBaseState currState;
        public Rigidbody rb;

        [Space(25)]
        [Header("State Manager")]
        public BugFly bugFly;
        public BugFall bugFall;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
        }

        private void Start() {
            currState = bugFall;
            currState.EnterState(this);
        }

        private void Update() {
            currState.UpdateState(this);
        }

        public void SwitchState(BugBaseState newState) {
            currState.ExitState(this);
            currState = newState;
            currState.EnterState(this);
        }
    }

    public abstract class BugBaseState
    {
        public abstract void EnterState(BugStateManager manager);
        public abstract void UpdateState(BugStateManager manager);
        public abstract void ExitState(BugStateManager manager);
    }
}