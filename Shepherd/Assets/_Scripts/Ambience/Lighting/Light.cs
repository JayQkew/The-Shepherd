using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class Light : ProfileData
    {
        public static float TotalIntensity;
        public static int Count;
        public Color color = Color.white;
        public float intensity = 1f;

        public Light() {
            TotalIntensity = 1;
            Count = 1;
        }

        public Light(Color color, float intensity) {
            this.color = color;
            this.intensity = intensity;
        }

        public Light Clone() {
            Light newLight = new Light (color, intensity);

            return newLight;
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