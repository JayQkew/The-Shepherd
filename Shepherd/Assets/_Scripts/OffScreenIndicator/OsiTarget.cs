using System;
using UnityEngine;

namespace OffScreenIndicator
{
    public class OsiTarget : MonoBehaviour
    {
        public string description;
        private void Start() {
            OsiManager.Instance.AddTarget(this);
        }
    }
}
