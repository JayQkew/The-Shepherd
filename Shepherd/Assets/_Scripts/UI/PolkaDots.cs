using System;
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
        [SerializeField] private Material polkaDotMaterial;
        public float fill;

        private void Awake() {
            anim = GetComponent<Animator>();
        }

        private void Start() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Update() {
            polkaDotMaterial.SetFloat(Fill, fill);
        }

        private void OnDestroy() {
#if UNITY_EDITOR
            polkaDotMaterial.SetFloat(Fill, 0);
#endif
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void FadeOutTrigger() => anim.SetTrigger(FadeOut);

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode) {
            anim.SetTrigger(FadeIn);
        }
    }
}
