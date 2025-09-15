using UnityEngine;

namespace Creatures
{
    [CreateAssetMenu(fileName = "Animal Data",menuName = "Creatures/AnimalData")]

    public class AnimalData : ScriptableObject
    {
        public MinMax mass;
        public bool useGravity;
        public float linearDamping;
    }
}
