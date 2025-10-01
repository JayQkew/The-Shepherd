using System;
using UnityEngine;

namespace OffScreenIndicator
{
    public class OsiTarget : MonoBehaviour
    {
        public string description;
        public bool subscribeOnStart = false;

        private void Start() {
            if (subscribeOnStart) {
                Subscribe();
            }
        }

        public void Subscribe() {
            OsiManager.Instance.AddTarget(this);
        }

        public void Unsubscribe() {
            OsiManager.Instance.RemoveTarget(this);
        }
    }
}