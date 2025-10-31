using UnityEngine;

namespace SpriteSystem
{
    public class GUI : MonoBehaviour
    {
        [SerializeField] private Material spriteShader;
        private void Start() {
            SpriteManager.Instance.AddGUI(transform, spriteShader);
        }

        private void OnDestroy() {
            SpriteManager.Instance.RemoveGUI(transform);
        }
    }
}
