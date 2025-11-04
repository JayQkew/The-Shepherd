using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class FMODEvents : MonoBehaviour
    {
        public static FMODEvents Instance { get; private set; }
        
        [field: Header("Player Events")]
        [field: SerializeField] public EventReference grassRun { get; private set; }
        [field: SerializeField] public EventReference grassWalk { get; private set; }
        [field: SerializeField] public EventReference bark { get; private set; }
        
        [field: Header("Animal Events")]
        [field: SerializeField] public EventReference poop { get; private set; }
        
        [field: Header("Sheep Events")] 
        [field: SerializeField] public EventReference sheepMeh { get; private set; }
        [field: SerializeField] public EventReference sheepEat { get; private set; }
        [field: SerializeField] public EventReference sheepPoof { get; private set; }
        
        [field: Header("Ducken Events")]
        [field: SerializeField] public EventReference duckenSound { get; private set; }
        [field: SerializeField] public EventReference duckSound { get; private set; }
        [field: SerializeField] public EventReference chickenSound { get; private set; }
        [field: SerializeField] public EventReference duckenWalk { get; private set; }
        [field: SerializeField] public EventReference duckenChange { get; private set; }
        
        [field: Header("Fire Place Events")]
        [field: SerializeField] public EventReference fireCrackling { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }
    }
}
