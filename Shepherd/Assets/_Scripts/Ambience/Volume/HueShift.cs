using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class HueShift : ProfileData
    {
        [Tooltip("< 0 is cooler & > 0 is warmer")]
        public float value = 0;
    }
}
