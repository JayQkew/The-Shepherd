using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class StartScreenUI : MonoBehaviour
    {
        private static readonly int FadeOut = Animator.StringToHash("FadeOut");
        [SerializeField] private Animator fadeAnim;
        [SerializeField] private float fadeOutTime;
        private WaitForSeconds WaitForSeconds => new (fadeOutTime);
        
        public void ExitGame() => Application.Quit();

        public void StartGame() {
            fadeAnim.SetTrigger(FadeOut);
            StartCoroutine(SwitchScenes());
        }

        private IEnumerator SwitchScenes() {
            yield return WaitForSeconds;
            SceneManager.LoadScene("Main Scene");
        }
    }
}
