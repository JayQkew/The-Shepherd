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
        private List<StudioEventEmitter> eventEmitters = new();
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

        public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject) {
            StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
            emitter.EventReference = eventReference;
            eventEmitters.Add(emitter);
            return emitter;
        }

        private void CleanUp() {
            foreach (EventInstance instance in eventInstances) {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
            }

            foreach (StudioEventEmitter emitter in eventEmitters) {
                emitter.Stop();
            }
        }

        private void OnDestroy() {
            CleanUp();
        }
    }
}
