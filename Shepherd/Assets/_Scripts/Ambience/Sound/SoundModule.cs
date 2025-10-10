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

        [Space(15)]
        [SerializeField] private Wind windProfileData;
        [SerializeField] private Rain rainProfileData;
        [SerializeField] private Thunder thunderProfileData;
        [SerializeField] private Leaves leavesProfileData;
        [SerializeField] private Birds birdProfileData;
        [SerializeField] private Insects insectsProfileData;
        
        public override void TotalProfiles() {
            Wind tempWind = new Wind();
            Rain tempRain = new Rain();
            Thunder tempThunder = new Thunder();
            Leaves tempLeaves = new Leaves();
            Birds tempBirds = new Birds();
            Insects tempInsects = new Insects();

            foreach (Profile profile in Profiles) {
                SoundProfile soundProfile = profile as SoundProfile;

                if (soundProfile == null) {
                    Debug.LogWarning("SoundProfile profile not in SoundProfile");
                    continue;
                }

                ProfileData[] profileDatas = soundProfile.GetProfileDatas();

                foreach (ProfileData profileData in profileDatas) {
                    if (profileData.Use) {
                        if (profileData is Wind windData) ProcessWind(windData, tempWind);
                        else if (profileData is Rain rainData) ProcessRain(rainData, tempRain);
                        else if (profileData is Thunder thunderData) ProcessThunder(thunderData, tempThunder);
                        else if (profileData is Leaves leavesData) ProcessLeaves(leavesData, tempLeaves);
                        else if (profileData is Birds birdData) ProcessBirds(birdData, tempBirds);
                        else if (profileData is Insects insectData) ProcessInsect(insectData, tempInsects);
                    }
                }
            }
        }

        public override void ApplyProfiles() {
            throw new NotImplementedException();
        }
        
        public void ProcessWind(Wind windData, Wind tempProfileData){}
        public void ProcessRain(Rain rainData, Rain tempProfileData){}
        public void ProcessThunder(Thunder thunderData, Thunder tempProfileData){}
        public void ProcessLeaves(Leaves leavesData, Leaves tempProfileData){}
        public void ProcessBirds(Birds birdData, Birds tempProfileData){}
        public void ProcessInsect(Insects insectData, Insects tempProfileData){}

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
