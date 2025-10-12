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
        
        public override void Init() {
            sounds = new AmbientSound[data.sounds.Length];
            for (int i = 0; i < data.sounds.Length; i++) {
                sounds[i] = data.sounds[i].Clone();
                sounds[i].Init();
            }
        }

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
                        if (profileData is Wind windData) windData.Process(tempWind);
                        else if (profileData is Rain rainData) rainData.Process(tempRain);
                        else if (profileData is Thunder thunderData) thunderData.Process(tempThunder);
                        else if (profileData is Leaves leavesData) leavesData.Process(tempLeaves);
                        else if (profileData is Birds birdData) birdData.Process(tempBirds);
                        else if (profileData is Insects insectData) insectData.Process(tempInsects);
                    }
                }
            }

            windProfileData = tempWind;
            rainProfileData = tempRain;
            thunderProfileData = tempThunder;
            leavesProfileData = tempLeaves;
            birdProfileData = tempBirds;
            insectsProfileData = tempInsects;

            base.TotalProfiles();
        }

        public override void ApplyProfiles() {
            ApplySoundProfile(windProfileData, Wind.Count);
            ApplySoundProfile(rainProfileData, Rain.Count);
            ApplySoundProfile(thunderProfileData, Thunder.Count);
            ApplySoundProfile(leavesProfileData, Leaves.Count);
            ApplySoundProfile(birdProfileData, Birds.Count);
            ApplySoundProfile(insectsProfileData, Insects.Count);
        }

        public void ApplySoundProfile(Sound sound, int count) {
            AmbientSoundType soundType = sound.SoundType;
            EventInstance eventInstance = sounds[(int)soundType].EventInstance;

            if (count > 0) {
                eventInstance.getPlaybackState(out PLAYBACK_STATE playbackState);

                if (playbackState == PLAYBACK_STATE.STOPPED) {
                    eventInstance.start();
                }
                else if (playbackState == PLAYBACK_STATE.PLAYING) {
                    eventInstance.getPaused(out bool paused);
                    if (paused) eventInstance.setPaused(false);
                }
            }
            else {
                eventInstance.getPlaybackState(out PLAYBACK_STATE playbackState);

                if (playbackState == PLAYBACK_STATE.PLAYING) eventInstance.setPaused(true);
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