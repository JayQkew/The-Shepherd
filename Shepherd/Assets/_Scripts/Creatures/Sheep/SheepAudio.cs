using UnityEngine;

namespace Creatures.Sheep
{
    public class SheepAudio : MonoBehaviour
    {
        [SerializeField] private Sheep sheep;
        public void PlayMeh() => sheep.PlayMeh();
    }
}
