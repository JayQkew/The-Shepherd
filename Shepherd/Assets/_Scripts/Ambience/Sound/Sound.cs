namespace Ambience
{
    public abstract class Sound : ProfileData
    {
        public abstract AmbientSoundType SoundType { get; }
        public float Intensity;
    }
}