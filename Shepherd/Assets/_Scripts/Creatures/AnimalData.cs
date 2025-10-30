using UnityEngine;
using Utilities;

namespace Creatures
{
    [CreateAssetMenu(fileName = "Animal Data",menuName = "Creatures/AnimalData")]

    public class AnimalData : ScriptableObject
    {
        [Header("Animal Data")]
        public MinMax mass;
        public bool useGravity;
        public float linearDamping;
    }
}
