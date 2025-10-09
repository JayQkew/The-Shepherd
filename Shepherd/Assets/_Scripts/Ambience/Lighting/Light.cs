using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class Light : ProfileData
    {
        public Color color = Color.white;
        public float intensity = 1f;
    }
}
