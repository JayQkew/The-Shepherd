using UnityEngine;
using Utilities;

namespace Creatures
{
    public class AnimalData : ScriptableObject
    {
        [Header("Animal Data")]
        public MinMax mass;
        public bool useGravity;
        public float linearDamping;
    }
}
