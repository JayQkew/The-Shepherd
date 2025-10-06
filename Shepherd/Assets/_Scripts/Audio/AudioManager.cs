using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private List<EventInstance> eventInstances = new();
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        public void PlayOneShot(EventReference sound) => RuntimeManager.PlayOneShot(sound);
        
        public void PlayOneShot(EventReference sound, Vector3 worldPos) =>
        RuntimeManager.PlayOneShot(sound, worldPos);

        public EventInstance CreateInstance(EventReference eventReference) {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            eventInstances.Add(eventInstance);
            return eventInstance;
        }

        private void CleanUp() {
            foreach (EventInstance instance in eventInstances) {
                instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                instance.release();
            }
        }

        private void OnDestroy() {
            CleanUp();
        }
    }
}
