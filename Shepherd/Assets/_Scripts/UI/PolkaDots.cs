using System;
using UnityEngine;

namespace UI
{
    public class PolkaDots : MonoBehaviour
    {
        private static readonly int Fill = Shader.PropertyToID("_Fill");
        [SerializeField] private Material polkaDotMaterial;
        public float fill;

        private void Update() {
            polkaDotMaterial.SetFloat(Fill, fill);
        }
    }
}
