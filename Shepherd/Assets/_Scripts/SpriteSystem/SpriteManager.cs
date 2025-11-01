using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace SpriteSystem
{
    public class SpriteManager : MonoBehaviour
    {
        public static SpriteManager Instance { get; private set; }
        [SerializeField] private Quaternion spriteRotation;
        [SerializeField] private List<Transform> guis;
        [SerializeField] private Material defaultMat;

        private void Awake() {
            if (Instance == null) Instance = this;
            else if (Instance != this) Destroy(gameObject);
        }

        public void AddGUI(Transform t, Material shadermaterial) {
            guis.Add(t);
            SpriteRenderer parentSr = t.GetComponent<SpriteRenderer>();
            SpriteRenderer[] childrenSr = t.gameObject.GetComponentsInChildren<SpriteRenderer>();

            if (parentSr) ProcessGUI(parentSr, shadermaterial);
            if (childrenSr.Length > 0) {
                SortingGroup sg = t.GetComponent<SortingGroup>();
                if(!sg) sg = t.AddComponent<SortingGroup>();
            
                sg.sortingOrder = 0;
                foreach (SpriteRenderer sr in childrenSr) {
                    ProcessGUI(sr, shadermaterial);
                }
            }
        
            t.transform.rotation = spriteRotation; // only rotate the parent (not the children seperately)
        }

        private void ProcessGUI(SpriteRenderer sr, Material shaderMaterial) {
            sr.shadowCastingMode = ShadowCastingMode.On;
            sr.receiveShadows = true;
            sr.material = shaderMaterial == null ? defaultMat : shaderMaterial;
        }

        public void RemoveGUI(Transform t) => guis.Remove(t);

        private void OnValidate() {
            if (Instance == null) Instance = this;
        }
    }
}
