using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct Timer
{
    public float maxTime;
    public float currTime { get; private set; }
    
    public Timer(float maxTime) {
        this.maxTime = maxTime;
        this.currTime = 0f;
    }
    
    public void Update() {
        if (currTime < maxTime) {
            currTime += Time.deltaTime;
            currTime = Mathf.Clamp(currTime, 0, maxTime);
        }
    }
    
    public void Reset() {
        currTime = 0f;
    }
    
    public bool IsFinished => currTime >= maxTime;
    
    public float Progress => maxTime > 0 ? currTime / maxTime : 0f;
    
    public void SetMaxTime(float newMaxTime, bool resetTimer = true) {
        maxTime = newMaxTime;
        if (resetTimer) {
            Reset();
        }
    }
}
