using System;
using UnityEngine;

namespace Ambience
{
    public class AmbienceManager : MonoBehaviour
    {
        public static AmbienceManager Instance { get; private set; }
        
        public AmbientLighting lighting;
        [Space(15)]
        public AmbientVolume volume;
        [Space(15)]
        public AmbientAudio[] audios;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(this);
            }
            
            volume.Init();
        }

        private void Update() {
            lighting.UpdateLighting();
            volume.UpdateVolume();
        }
    }
}
