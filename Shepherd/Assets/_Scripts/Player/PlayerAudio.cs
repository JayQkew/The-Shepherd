using Audio;
using UnityEngine;

namespace Player
{
    public class PlayerAudio : MonoBehaviour
    {
        private AudioManager audioManager;
        private FMODEvents fmodEvents;

        private void Start() {
            audioManager = AudioManager.Instance;
            fmodEvents = FMODEvents.Instance;
        }

        public void PlayGrassRun() => audioManager.PlayOneShot(fmodEvents.grassRun);
        public void PlayGrassWalk() => audioManager.PlayOneShot(fmodEvents.grassWalk);
    }
}
