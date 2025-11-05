using System;
using Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PolkaDots : MonoBehaviour
    {
        private Animator anim;
        private static readonly int Fill = Shader.PropertyToID("_Fill");
        private static readonly int FadeIn = Animator.StringToHash("FadeIn");
        private static readonly int FadeOut = Animator.StringToHash("FadeOut");
        private static readonly int StartScene = Animator.StringToHash("StartScene");
        public float fill;

        private void Awake() {
            anim = GetComponent<Animator>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Start() {
            anim.SetBool(StartScene, SceneManager.GetActiveScene().name == "Start Menu");
        }

        private void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void FadeOutTrigger() => anim.SetTrigger(FadeOut);

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode) {
            anim.SetTrigger(FadeIn);
            BarnSceneTrigger barnSceneTrigger = FindFirstObjectByType<BarnSceneTrigger>();
            barnSceneTrigger.polkaDots = this;
        }
    }
}
