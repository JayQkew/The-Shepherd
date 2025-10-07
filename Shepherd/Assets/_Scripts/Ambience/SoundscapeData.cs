using UnityEngine;

namespace Ambience
{
    [CreateAssetMenu(fileName = "NewSoundscapeData", menuName = "Ambience/Soundscape")]
    public class SoundscapeData : ScriptableObject
    {
        public AmbientSound[] sounds;
    }
}
