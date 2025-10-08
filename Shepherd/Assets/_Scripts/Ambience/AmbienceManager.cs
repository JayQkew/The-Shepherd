using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ambience
{
    public class AmbienceManager : MonoBehaviour
    {
        public static AmbienceManager Instance { get; private set; }
        
        public LightingModule lightingModule;
        [Space(15)]
        public VolumeModule volumeModule;
        [Space(15)]
        public SoundModule soundModule;
        [Space(15)]
        public ParticlesModule particlesModule;
        [Space(15)]
        public List<AmbienceSource> sources = new();
        
        private List<Module> modules = new();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(this);
            }
            
            modules.Add(lightingModule);
            modules.Add(volumeModule);
            modules.Add(soundModule);
            modules.Add(particlesModule);
        }

        public void ProcessSources() {
            foreach (AmbienceSource source in sources) {
                source.Process(modules);
            }
        }

        private void Start() {
            volumeModule.Init();
            soundModule.Init();
        }

        private void Update() {
            lightingModule.UpdateLighting();
            volumeModule.UpdateVolume();
        }

        private void OnValidate() {
            lightingModule.OnValidate();
        }
    }

    public enum AmbienceType
    {
        Sound,
        Volume,
        Lighting,
        Particles
    }
}
