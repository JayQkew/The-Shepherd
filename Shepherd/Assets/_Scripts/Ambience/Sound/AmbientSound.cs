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
    }

    [Flags]
    public enum AmbientSoundType
    {
        None = 0,
        Wind = 1 << 0,
        Rain = 1 << 1,
        Thunder = 1 << 2,
        Leaves = 1 << 3,
        Birds = 1 << 4,
        Insects = 1 << 5,
    }
}
