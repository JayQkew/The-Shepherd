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

        private void Start() {
            ClimateManager.Instance.tempReceptors.Add(this);
        }

        public float CalcTemp() {
            currTemp = ClimateManager.Instance.globalTemp;

            foreach (TempAffector affector in affectors) {
                currTemp += affector.tempModifier;
            }
            onCalcTemp?.Invoke();
            return currTemp;
        }

        private void OnDestroy() {
            ClimateManager.Instance.tempReceptors.Remove(this);
        }
    }
}