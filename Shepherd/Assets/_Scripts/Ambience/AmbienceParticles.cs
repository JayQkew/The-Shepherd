using System;
using UnityEngine;

namespace Ambience
{
    public class AmbienceParticles : MonoBehaviour
    {
        [SerializeField] private Transform follow;
        [SerializeField] private Vector3 offset;

        private void Update() {
            transform.position = follow.position + offset;
        }

        private void OnValidate() {
            if (follow != null) {
                transform.position = follow.position + offset;
            }
        }
    }
}
