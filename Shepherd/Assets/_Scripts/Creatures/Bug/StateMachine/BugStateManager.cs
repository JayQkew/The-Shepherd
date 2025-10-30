using System;
using Climate;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Creatures
{
    public class BugStateManager : Bug
    {
        public BugBaseState currState;

        [Space(25)]
        [Header("State Manager")]
        public BugFly bugFly;
        public BugFall bugFall;

        private float noiseSeed;

        protected override void Awake() {
            base.Awake();
            noiseSeed = Random.value * 100f;
        }

        protected override void Start() {
            base.Start();
            currState = bugData.fallSeason.HasFlag(ClimateManager.Instance.currSeason.season) ?
                bugFall : bugFly;
            currState.EnterState(this);
            
            foreach (Season season in ClimateManager.Instance.seasons) {
                if (bugData.fallSeason.HasFlag(season.season)) {
                    season.onSeasonBegin.AddListener(() => SwitchState(bugFall));
                } else if (bugData.flySeason.HasFlag(season.season)) {
                    season.onSeasonBegin.AddListener(() => SwitchState(bugFly));
                }
            }
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