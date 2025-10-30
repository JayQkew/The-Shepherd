using UnityEngine;

namespace Creatures
{
    [CreateAssetMenu(fileName = "BugFallData", menuName = "Bug/FallData")]
    public class BugFallData : ScriptableObject
    {
        [Header("Walk Settings")]
        public float walkSpeed = 1.5f;
        public float turnSpeed = 3f;
        public float wanderIntervalMin = 2f;
        public float wanderIntervalMax = 5f;

        [Header("Idle Settings")]
        public float idleDurationMin = 1f;
        public float idleDurationMax = 3f;
    }
}
