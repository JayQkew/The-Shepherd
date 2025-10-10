using System;
using System.Collections.Generic;
using FMOD.Studio;
using Unity.VisualScripting;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class SoundModule : Module
    {
        public override AmbienceType AmbienceType => AmbienceType.Sound;

        [SerializeField] private SoundscapeData data;
        [SerializeField] private AmbientSound[] sounds;
        
        public override void TotalProfiles() {
        }

        public override void ApplyProfiles() {
        }

        public void Init() {
            sounds = new AmbientSound[data.sounds.Length];
            for (int i = 0; i < data.sounds.Length; i++) {
                sounds[i] = data.sounds[i].Clone();
            }
        }

        public void PlaySoundRequests(Dictionary<AmbientSoundType, float> soundRequests) {
            StopAllSounds();
            
            foreach (KeyValuePair<AmbientSoundType,float> soundRequest in soundRequests) {
                PlaySound(soundRequest.Key, soundRequest.Value);
            }
        }
        
        public void PlaySound(AmbientSoundType soundType, float intensity) {
            foreach (AmbientSound sound in sounds) {
                if (sound.ambienceType == soundType) {
                    sound.EventInstance.setParameterByName("intensity", intensity);
                    sound.EventInstance.start();
                    return;
                }
            }
        }

        private void StopAllSounds() {
            foreach (AmbientSound sound in sounds) {
                sound.EventInstance.stop(STOP_MODE.IMMEDIATE);
            }
        }
    }
}
