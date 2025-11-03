using UnityEngine;

namespace UI
{
    public class PersistantCanvas : MonoBehaviour
    {
        private static PersistantCanvas _instance;

        private void Awake() {
            if (_instance != null && _instance != this) 
            {
                Debug.LogWarning("More than one instance of PersistantCanvas");
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
