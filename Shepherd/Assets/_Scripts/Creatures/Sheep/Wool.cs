using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Creatures.Sheep
{
    [Serializable]
    public class Wool
    {
        public float woolValue;
        private float prevWool;
        [SerializeField] private Timer woolTimer;
        [SerializeField] private UnityEvent[] woolEvents = new UnityEvent[3];

        public Wool(MinMax woolTime) {
            woolTimer.SetMaxTime(woolTime.RandomValue());
            prevWool = woolValue;
        }
        
        public void WoolUpdate() {
            woolTimer.Update();
            woolValue = woolTimer.Progress;
            WoolCheck();
        }
        
        /// <summary>
        /// Checks for when the wool meets a threshold
        /// </summary>
        private void WoolCheck() {
            if (woolValue <= 0.1f) {
                woolEvents[0]?.Invoke();
            }
            if (prevWool < 0.3f && woolValue >= 0.3f) {
                woolEvents[1]?.Invoke();
            }
            if (prevWool < 0.6f && woolValue >= 0.6f) {
                woolEvents[2]?.Invoke();
            }
            
            prevWool = woolValue;
        }
    }
}
