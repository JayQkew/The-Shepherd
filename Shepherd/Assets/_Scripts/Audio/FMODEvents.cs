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
