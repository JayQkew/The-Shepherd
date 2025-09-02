using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Creatures
{
    [CreateAssetMenu(fileName = "Animal Data",menuName = "Creatures/AnimalData")]

    public class AnimalData : ScriptableObject
    {
        public MinMax mass;
        public bool useGravity;
        public float linearDamping;
    }
}
