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
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
            
            UnityEngine.SceneManagement.Scene tempScene = SceneManager.CreateScene("TempScene");
            foreach (GameObject go in GetDontDestroyOnLoadObjects())
            {
                SceneManager.MoveGameObjectToScene(go, tempScene);
            }
            SceneManager.UnloadSceneAsync(tempScene);
        }
        
        private GameObject[] GetDontDestroyOnLoadObjects()
        {
            GameObject temp = new GameObject("Temp");
            DontDestroyOnLoad(temp);

            UnityEngine.SceneManagement.Scene dontDestroyOnLoadScene = temp.scene;
            DestroyImmediate(temp);

            return dontDestroyOnLoadScene.GetRootGameObjects();
        }
    }
}
