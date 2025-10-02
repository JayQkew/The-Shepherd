using System;
using System.Collections.Generic;
using UnityEngine;

namespace Climate
{
    public class TempAffector : MonoBehaviour
    {
        public float tempModifier;
        public float radius;
        [SerializeField] private LayerMask layerMask;
        [Space(20)]
        [SerializeField] private bool showGizmos;
        private Collider[] colliders = new Collider[4];
        private HashSet<TempReceptor> affectedReceptors = new();
        

        private void Start() {
            ClimateManager.Instance.tempAffectors.Add(this);
        }

        public void FindReceptors() {
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
                    affectedReceptors.Add(receptor);
                }
            }
        }

        private void OnDisable() {
            foreach (TempReceptor receptor in affectedReceptors) {
                if (receptor != null) {
                    receptor.affectors.Remove(this);
                }
            }
            affectedReceptors.Clear();
        }

        private void OnDestroy() {
            ClimateManager.Instance.tempAffectors.Remove(this);
            foreach (TempReceptor receptor in affectedReceptors) {
                if (receptor != null) {
                    receptor.affectors.Remove(this);
                }
            }
        }

        private void OnDrawGizmosSelected() {
            if (showGizmos) {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, radius);
            }
        }
    }
}
