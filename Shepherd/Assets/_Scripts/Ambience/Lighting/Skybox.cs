using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class Skybox : ProfileData
    {
        public static int Count;
        public Color color = Color.white;

        public Skybox() {
            Count = 0;
        }

        public Skybox Clone() {
            Skybox newSkybox = new Skybox
            {
                color = color
            };

            return newSkybox;
        }

        protected override void ProcessInternal(ProfileData tempData) {
            Skybox tempProfileData = tempData as Skybox;
            if (Count == 1) {
                tempProfileData.color = color;
            } else {
                // Otherwise, lerp with proper weighting
                float weight = 1f / Count;
                tempProfileData.color = Color.Lerp(tempProfileData.color, color, weight);
            }
            tempProfileData.color.a = 1;
            Count++;
        }
    }
}