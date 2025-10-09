using System;
using System.Collections.Generic;
using TimeSystem;
using Unity.VisualScripting;
using UnityEngine;
using Timer = Utilities.Timer;

namespace Climate
{
    public class ClimateManager : MonoBehaviour
    {
        public static ClimateManager Instance { get; private set; }

        public float globalTemp;
        private float targetTemp;
        [SerializeField] private Timer tempThrottle;
        public HashSet<TempAffector> tempAffectors = new();
        public HashSet<TempReceptor> tempReceptors = new();

        [Space(20)]
        [Header("Seasons")]
        public SeasonName currSeason;
        public SeasonData seasonData;
        [SerializeField] private Season[] seasons;

        [Space(20)]
        [Header("Weather")]
        public WeatherManager weatherManager;

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
            seasons = (Season[])seasonData.seasons.Clone();


            globalTemp = seasons[(int)currSeason].SetTemp();
            timeManager.onDayPhaseChange.AddListener(SeasonCheck);
            currSeason = seasons[0].season;
            seasons[0].Begin();

            foreach (TempAffector affector in tempAffectors) {
                affector.FindReceptors();
            }

            foreach (TempReceptor receptor in tempReceptors) {
                receptor.CalcTemp();
            }
        }

        private void Update() {
            if (TimeManager.Instance.currPhase == DayPhaseName.Sunrise) {
                globalTemp = Mathf.Lerp(globalTemp, targetTemp, TimeManager.Instance.currDayPhase.timer.Progress);
            }

            tempThrottle.Update();
            if (tempThrottle.IsFinished) {
                foreach (TempAffector affector in tempAffectors) {
                    affector.FindReceptors();
                }

                foreach (TempReceptor receptor in tempReceptors) {
                    receptor.CalcTemp();
                }

                tempThrottle.Reset();
            }
        }

        private void SeasonCheck() {
            if (timeManager.dayCount % 3 == 0 && timeManager.dayCount != 0) {
                seasons[(int)currSeason].End();
                int nextSeason = ((int)currSeason + 1) % 4;
                currSeason = (SeasonName)nextSeason;
                seasons[nextSeason].Begin();
            }

            targetTemp = seasons[(int)currSeason].SetTemp();
        }
    }
}