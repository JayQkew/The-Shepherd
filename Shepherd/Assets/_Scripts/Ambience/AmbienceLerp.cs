using System;
using UnityEngine;
using Utilities;

namespace Ambience
{
    [Serializable]
    public class AmbienceLerp<T>
    {
        [SerializeField] private bool lerping;
        [SerializeField] private Timer timer;
        [SerializeField] private T currValue;
        [SerializeField] private T targetValue;
        
        public T CurrentValue => currValue;
        public bool IsLerping => lerping;

        public AmbienceLerp(float lerpTime, T initialValue) {
            timer = new Timer(lerpTime);
            currValue = initialValue;
            targetValue = initialValue;
            lerping = false;
        }

        public void StartLerp(T target) {
            bool needsLerp = false;

            if (currValue is float floatCurr && target is float floatTarget) {
                needsLerp = !Mathf.Approximately(floatCurr, floatTarget);
            }
            else if (currValue is Color colorCurr && target is Color colorTarget) {
                needsLerp = !colorTarget.Equals(colorCurr);
            }

            if (needsLerp) {
                targetValue = target;
                lerping = true;
                timer.Reset();
            }
        }

        public void Update() {
            if (!lerping) return;
            
            timer.Update();

            if (currValue is float && targetValue is float) {
                float curr = (float)(object)currValue;
                float target = (float)(object)targetValue;
                currValue = (T)(object)Mathf.Lerp(curr, target, timer.Progress);
            }
            else if (currValue is Color && targetValue is Color) {
                Color curr = (Color)(object)currValue;
                Color target = (Color)(object)targetValue;
                currValue = (T)(object)Color.Lerp(curr, target, timer.Progress);
            }

            if (timer.IsFinished) {
                lerping = false;
                currValue = targetValue;
            }
        }
        
        public void SetImmediate(T value) {
            currValue = value;
            targetValue = value;
            lerping = false;
        }
    }
}