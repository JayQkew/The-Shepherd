using System;
using System.Collections.Generic;
using TimeSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
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
        public SeasonData seasonData;

        public Season currSeason;
        [SerializeField] private Season[] seasons;

        [Space(20)]
        [Header("Weather")]
        public Weather currWeather;

        private TimeManager timeManager;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }

            seasons = new Season[seasonData.seasons.Length];
            for (int i = 0; i < seasonData.seasons.Length; i++) {
                seasons[i] = seasonData.seasons[i].Clone();
            }
        }

        private void Start() {
            timeManager = TimeManager.Instance;

            currSeason = seasons[0];
            currWeather = currSeason.GetWeather();
            globalTemp = currSeason.SetTemp() + currWeather.tempDelta;
            
            currSeason.Begin();

            timeManager.onDayPhaseChange.AddListener(SeasonCheck);

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
                currSeason.End();
                int nextSeason = ((int)currSeason.season + 1) % 4;
                currSeason = seasons[nextSeason];
                currSeason.Begin();
            }
            
            // WeatherCheck();
            SetTemp();
        }

        private void WeatherCheck() {
            currWeather.End();
            currWeather = currSeason.GetWeather();
            currWeather.Begin();
        }

        private void SetTemp() {
            targetTemp = currSeason.SetTemp() + currWeather.tempDelta;
        }
    }
}