using System;
using UnityEngine;

namespace OffScreenIndicator
{
    public class OsiTarget : MonoBehaviour
    {
        public Transform targetTransform;
        public string description;
        public bool subscribeOnStart = false;
        public float distance;

        private void Start() {
            if (targetTransform == null) {
                targetTransform = transform;
            }
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