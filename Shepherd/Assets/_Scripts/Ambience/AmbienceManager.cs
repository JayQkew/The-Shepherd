using System.Collections.Generic;
using UnityEngine;

namespace Ambience
{
    public class AmbienceManager : MonoBehaviour
    {
        public static AmbienceManager Instance { get; private set; }
        
        public LightingModule lightingModule;
        [Space(25)]
        public VolumeModule volumeModule;
        [Space(25)]
        public SoundModule soundModule;
        [Space(25)]
        public ParticlesModule particlesModule;
        
        private List<AmbienceSource> sources = new();
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

        public void AddSource(AmbienceSource source) {
            sources.Add(source);
            source.DelegateProfiles(modules);
        }

        public void RemoveSources(AmbienceSource source) {
            sources.Remove(source);
            source.BanishProfiles(modules);
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
