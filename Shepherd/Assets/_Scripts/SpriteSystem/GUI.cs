using UnityEngine;

namespace SpriteSystem
{
    [ExecuteAlways]
    public class GUI : MonoBehaviour
    {
        [SerializeField] private Material spriteShader;

        private void OnEnable() {
            if (SpriteManager.Instance != null)
                SpriteManager.Instance.AddGUI(transform, spriteShader);
        }

        private void OnDisable() {
            if (SpriteManager.Instance != null)
                SpriteManager.Instance.RemoveGUI(transform);
        }

#if UNITY_EDITOR
        private void OnValidate() {
            if (SpriteManager.Instance != null && isActiveAndEnabled)
                SpriteManager.Instance.AddGUI(transform, spriteShader);
        }
#endif
    }
}