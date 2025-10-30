using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

        private float noiseSeed;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
            noiseSeed = Random.value * 100f;
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
        
        /// <summary>
        /// using perlin noise to generate a random direction
        /// </summary>
        public Vector3 WanderDirection(float perlinScale) {
            float x = Mathf.PerlinNoise(noiseSeed, Time.time * perlinScale) * 2f - 1f;
            float z = Mathf.PerlinNoise(noiseSeed + 10f, Time.time * perlinScale) * 2f - 1f;
            return new Vector3(x, 0, z).normalized;
        }
    }

    public abstract class BugBaseState
    {
        public abstract void EnterState(BugStateManager manager);
        public abstract void UpdateState(BugStateManager manager);
        public abstract void ExitState(BugStateManager manager);
    }
}