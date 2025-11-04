using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIFunctionality : MonoBehaviour
    {
        public bool dontDestroyOnLoad;

        private void Awake() {
            if (dontDestroyOnLoad) {
                DontDestroyOnLoad(gameObject);
            }
        }

        public void ToggleUI() {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void ChangeTimeScale(float timeScale) {
            Time.timeScale = timeScale;
        }

        public void ToggleTimeScale() {
            Time.timeScale = gameObject.activeSelf ? 0f : 1f;
        }

        public void ExitGame() {
            Application.Quit();
        }
    }
}
