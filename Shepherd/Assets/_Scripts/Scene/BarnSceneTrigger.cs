using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class BarnSceneTrigger : MonoBehaviour
    {
        private readonly WaitForSeconds waitForSeconds = new(2.2f);
        [SerializeField] private PolkaDots polkaDots;

        private bool isTransitioning;

        private void Start() {
            if (polkaDots == null) {
                polkaDots = FindFirstObjectByType<PolkaDots>();
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (isTransitioning) return;
            
            if (!other.CompareTag("Player")) return;
            
            isTransitioning = true;
            polkaDots.FadeOutTrigger();
            StartCoroutine(ChangeScenes());
        }

        private IEnumerator ChangeScenes() {
            yield return waitForSeconds;
            string currentScene = SceneManager.GetActiveScene().name;
            string targetScene = currentScene == "Barn" ? "Main Scene" : "Barn";
            
            SceneManager.LoadScene(targetScene);
        }
    }
}