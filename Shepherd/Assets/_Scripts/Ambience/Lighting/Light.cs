using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class Light : ProfileData
    {
        public static float TotalIntensity;
        public static int Count;
        public static float CalculatedIntensity => TotalIntensity / Count;
        public Color color = Color.white;
        public float intensity = 1f;
    }
}
