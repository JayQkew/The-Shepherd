using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class BarnSceneTrigger : MonoBehaviour
    {
        private readonly WaitForSeconds waitForSeconds = new (2.2f);
        [SerializeField] private PolkaDots polkaDots;

        private void Start() {
            if (polkaDots == null) {
                polkaDots = FindFirstObjectByType<PolkaDots>();
            }
        }

        private void OnTriggerEnter(Collider other) {
            polkaDots.FadeOutTrigger();
            StartCoroutine(ChangeScenes());
        }

        private IEnumerator ChangeScenes() {
            yield return waitForSeconds;
            SceneManager.LoadScene("Barn");
        }
    }
}
