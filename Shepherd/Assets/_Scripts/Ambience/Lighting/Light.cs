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

        public Light() {
            TotalIntensity = 0;
            Count = 1;
        }

        protected override void ProcessInternal(ProfileData tempData) {
            Light tempProfileData = tempData as Light;
            tempProfileData.color = Color.Lerp(tempProfileData.color, color, 0.5f);
            tempProfileData.color.a = 1;
            TotalIntensity += intensity;
            Count++;
        }
    }
}
