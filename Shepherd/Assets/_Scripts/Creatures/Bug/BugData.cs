using UnityEngine;

namespace Creatures
{
    [CreateAssetMenu(fileName = "BugData", menuName = "Creature/Bug")]
    public class BugData : ScriptableObject
    {
        [Header("Walk Settings")]
        public float walkSpeed = 5f;
        public float turnSpeed = 3f;
        public float wanderIntervalMin = 2f;
        public float wanderIntervalMax = 5f;

        [Header("Idle Settings")]
        public float idleDurationMin = 1f;
        public float idleDurationMax = 3f;
        
        [Header("Fly Settings")]
        public float flySpeed = 2f;
        public float flyTurnSpeed = 2f;
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
