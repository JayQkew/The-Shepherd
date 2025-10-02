using System;
using TimeSystem;
using UnityEngine;
using Utilities;

namespace Climate
{
    public class ClimateManager : MonoBehaviour
    {
        public static ClimateManager Instance { get; private set; }

        public float globalTemp;
        [SerializeField] private MinMax tempClamp;
        [Space(20)]
        public SeasonName currSeason;
        public Season[] seasons;

        private TimeManager timeManager;
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        private void Start() {
            timeManager = TimeManager.Instance;
            timeManager.onDayPhaseChange.AddListener(SeasonCheck);
        }

        private void SeasonCheck() {
            if (timeManager.dayCount % 3 == 0) {
                seasons[(int)currSeason].onSeasonEnd.Invoke();
                int nextSeason = ((int)currSeason + 1) % 4;
                currSeason = (SeasonName)nextSeason;
                seasons[nextSeason].onSeasonStart.Invoke();
            }
        }
    }
}
