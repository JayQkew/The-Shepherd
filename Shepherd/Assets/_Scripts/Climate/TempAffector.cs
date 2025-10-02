using System;
using UnityEngine;

namespace Climate
{
    public class TempAffector : MonoBehaviour
    {
        public float tempModifier;
        public float radius;
        [SerializeField] private LayerMask layerMask;
        private Collider[] colliders = new Collider[4];
        

        private void Start() {
            ClimateManager.Instance.tempAffectors.Add(this);
        }

        public void ScanReceptors() {
            int size;
            while (true) {
                size = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, layerMask);
                if (size < colliders.Length) break;
                colliders = new Collider[colliders.Length*2];
            }

            for (int i = 0; i < size; i++) {
                TempReceptor receptor = colliders[i].GetComponent<TempReceptor>();
                if (receptor != null) {
                    receptor.affectors.Add(this);
                }
            }
        }
    }
}
