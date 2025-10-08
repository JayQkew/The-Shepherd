using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ambience
{
    public class AmbienceManager : MonoBehaviour
    {
        public static AmbienceManager Instance { get; private set; }
        
        public Lighting lighting;
        [Space(15)]
        public Volume volume;
        [Space(15)]
        public Soundscape soundscape;
        [Space(15)]
        public Particles particles;
        [Space(15)]
        public List<AmbienceProfile> ambienceProfiles = new();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(this);
            }
        }

        private void Start() {
            volume.Init();
            soundscape.Init();
        }

        private void Update() {
            lighting.UpdateLighting();
            volume.UpdateVolume();
        }
    }
}
