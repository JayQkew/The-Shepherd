using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class SoundModule : Module
    {
        public override AmbienceType AmbienceType => AmbienceType.Sound;
        
        [SerializeField] private SoundscapeData data;
        [SerializeField] private AmbientSound[] sounds;

        public void Init() {
            sounds = data.sounds.Clone() as AmbientSound[];
            if (sounds != null) {
                foreach (AmbientSound ambientSound in sounds) {
                    ambientSound.Init();
                }
            }
            else {
                Debug.LogWarning("Ambience Sounds are null!");
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

        public void ConvertIntensity(float intensity) {
            throw new NotImplementedException();
        }
    }
}
