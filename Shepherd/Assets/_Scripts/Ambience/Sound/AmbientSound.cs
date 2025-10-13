using System;
using Audio;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Ambience
{
    [Serializable]
    public class AmbientSound
    {
        public AmbientSoundType ambienceType;
        public EventInstance EventInstance;
        [field: SerializeField] public EventReference EventReference { get; private set; }
        
        public void Init() {
            EventInstance = AudioManager.Instance.CreateInstance(EventReference);
        }

        public AmbientSound Clone() {
            AmbientSound newAmbientSound = new AmbientSound
            {
                ambienceType = ambienceType,
                EventReference = EventReference
            };
            
            return newAmbientSound;
        }
    }

    public enum AmbientSoundType
    {
        Wind = 0,
        Rain = 1,
        Thunder = 2,
        Leaves = 3,
        Birds = 4,
        Insects = 5
    }
}
