using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Climate
{
    public class TempReceptor : MonoBehaviour
    {
        public float currTemp;
        public HashSet<TempAffector> affectors = new();
        public UnityEvent onCalcTemp;
        public UnityEvent onTempChange;

        private void Start() {
            ClimateManager.Instance.tempReceptors.Add(this);
        }

        public float CalcTemp() {
            float newTemp  = ClimateManager.Instance.globalTemp;

            foreach (TempAffector affector in affectors) {
                newTemp += affector.tempModifier;
            }
            onCalcTemp?.Invoke();

            if (!Mathf.Approximately(newTemp, currTemp)) {
                onTempChange?.Invoke();
                currTemp = newTemp;
            }
            return currTemp;
        }

        private void OnDestroy() {
            ClimateManager.Instance.tempReceptors.Remove(this);
        }
    }
}