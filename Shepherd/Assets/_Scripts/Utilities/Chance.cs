
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    [Serializable]
    public class Chance
    {
        [Range(0, 1)] public float percentage;

        public bool Roll() {
            float roll = Random.Range(0f, 1f);
            return roll <= percentage;
        }

        public static bool RollChance(float percentage) {
            return Random.Range(0f, 1f) < percentage;
        }
    }
}
