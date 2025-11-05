using UnityEngine;

namespace SpriteSystem
{
    [ExecuteAlways]
    public class GUI : MonoBehaviour
    {
        [SerializeField] private Material spriteShader;

        private void Start() {
            if (SpriteManager.Instance != null) {
                SpriteManager.Instance.AddGUI(transform, spriteShader);
            } else {
                Debug.LogWarning($"SpriteManager Instance is null when enabling GUI on {name}. Make sure SpriteManager exists in the scene.");
            }
        }

        private void OnDisable() {
            if (SpriteManager.Instance != null)
                SpriteManager.Instance.RemoveGUI(transform);
        }
    }
}