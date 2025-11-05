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
        [SerializeField] private UnityEvent[] woolEvents = new UnityEvent[3];

        public void Init(SheepData data) {
            sheepData = data;
            if (!sheepData.timerSet) {
                sheepData.woolTimer.SetMaxTime(data.woolTime.RandomValue());
                sheepData.timerSet = true;
            }
            sheepData.prevWool = sheepData.woolTimer.Progress;
        }
        
        public void WoolUpdate() {
            sheepData.woolTimer.Update();
            WoolCheck();
        }
        
        /// <summary>
        /// Checks for when the wool meets a threshold
        /// </summary>
        private void WoolCheck() {
            float woolValue = sheepData.woolTimer.Progress;
            
            if (woolValue <= 0.1f) {
                woolEvents[0]?.Invoke();
            }
            if (sheepData.prevWool < 0.3f && woolValue >= 0.3f) {
                woolEvents[1]?.Invoke();
            }
            if (sheepData.prevWool < 0.6f && woolValue >= 0.6f) {
                woolEvents[2]?.Invoke();
            }
            
            sheepData.prevWool = woolValue;
        }
    }
}
