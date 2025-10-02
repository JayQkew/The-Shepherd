using System;
using System.Collections.Generic;
using UnityEngine;

namespace Climate
{
    public class TempReceptor : MonoBehaviour
    {
        public float currTemp;
        public HashSet<TempAffector> affectors = new();

        private void Start() {
            ClimateManager.Instance.tempReceptors.Add(this);
        }

        public float CalcTemp() {
            currTemp = ClimateManager.Instance.globalTemp;
            
            foreach (TempAffector affector in affectors) {
                currTemp += affector.tempModifier;
            }
            
            return currTemp;
        }
    }
}
