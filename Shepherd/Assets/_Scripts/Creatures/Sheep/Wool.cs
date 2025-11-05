using System;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Creatures.Sheep
{
    [Serializable]
    public class Wool
    {
        private SheepData sheepData;
        private float prevWool;
        [SerializeField] private UnityEvent[] woolEvents = new UnityEvent[3];

        public void Init(MinMax woolTime, SheepData data) {
            sheepData = data;
            if (!sheepData.timerSet) {
                sheepData.woolTimer.SetMaxTime(woolTime.RandomValue());
                sheepData.timerSet = true;
            } else {
                // Restore timer state from saved values
                sheepData.woolTimer.maxTime = sheepData.savedWoolTimerMax;
                sheepData.woolTimer.currTime = sheepData.savedWoolTimerCurrent;
            }
            prevWool = sheepData.woolValue;
        }
        
        public void WoolUpdate() {
            sheepData.woolTimer.Update();
            sheepData.woolValue = sheepData.woolTimer.Progress;
            sheepData.savedWoolTimerCurrent = sheepData.woolTimer.currTime;
            sheepData.savedWoolTimerMax = sheepData.woolTimer.maxTime;

            WoolCheck();
        }
        
        /// <summary>
        /// Checks for when the wool meets a threshold
        /// </summary>
        private void WoolCheck() {
            if (sheepData.woolValue <= 0.1f) {
                woolEvents[0]?.Invoke();
            }
            if (prevWool < 0.3f && sheepData.woolValue >= 0.3f) {
                woolEvents[1]?.Invoke();
            }
            if (prevWool < 0.6f && sheepData.woolValue >= 0.6f) {
                woolEvents[2]?.Invoke();
            }
            
            prevWool = sheepData.woolValue;
        }
    }
}
