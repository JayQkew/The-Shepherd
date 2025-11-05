using UnityEngine;

namespace Scene
{
    public class PersistantManagers : MonoBehaviour
    {
        private static PersistantManagers _instance;
        private void Awake() {
            if (_instance != null && _instance != this) {
                Debug.LogWarning("More than one instance of PersistantManagers found! **DEMOLISHED** the imposter");
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
