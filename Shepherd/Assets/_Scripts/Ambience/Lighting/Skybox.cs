using System;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class Skybox : ProfileData
    {
        public Color color = Color.white;

        public Skybox Clone() {
            Skybox newSkybox = new Skybox
            {
                color = color
            };

            return newSkybox;
        }

        protected override void ProcessInternal(ProfileData tempData) {
            Skybox tempProfileData = tempData as Skybox;
            tempProfileData!.color = Color.Lerp(tempProfileData.color, color, 0.5f);
            tempProfileData.color.a = 1;
        }
    }
}