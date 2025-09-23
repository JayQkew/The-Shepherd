using System;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public struct Timer
    {
        public float maxTime;
        public float currTime;
    
        public Timer(float maxTime) {
            this.maxTime = maxTime;
            currTime = 0f;
        }
    
        public void Update() {
            if (currTime < maxTime) {
                currTime += Time.deltaTime;
                currTime = Mathf.Clamp(currTime, 0, maxTime);
            }
        }
    
        public void Reset() => currTime = 0f;
    
        public bool IsFinished => currTime >= maxTime;
    
        public float Progress => maxTime > 0 ? currTime / maxTime : 0f;
    
        /// <summary>
        /// sets the new max time and also resets the timer
        /// </summary>
        public void SetMaxTime(float newMaxTime, bool resetTimer = true) {
            maxTime = newMaxTime;
            if (resetTimer) Reset();
        }
    }
}
