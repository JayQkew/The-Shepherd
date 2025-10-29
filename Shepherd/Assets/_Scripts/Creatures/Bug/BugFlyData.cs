using UnityEngine;

namespace Creatures
{
    [CreateAssetMenu(fileName = "BugFlyData", menuName = "Bug/FlyData")]
    public class BugFlyData : ScriptableObject
    {
        [Header("Movement Settings")]
        public float speed = 2f;
        public float turnSpeed = 2f;
        public float wanderStrength = 1f;

        [Header("Hover Settings")]
        public float targetHeight = 2f;
        public float heightAdjustSpeed = 0.5f;
        public float raycastDistance = 10f;
        public LayerMask groundMask;

        [Header("Flutter Settings")]
        public float flutterAmplitude = 0.5f;
        public float flutterFrequency = 5f;
    }
}
