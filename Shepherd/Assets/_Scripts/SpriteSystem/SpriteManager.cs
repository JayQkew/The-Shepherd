using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance { get; private set; }
    [SerializeField] private Quaternion spriteRotation;
    [SerializeField] private List<Transform> guis;
    [SerializeField] private Material spriteShadowMat;

    private void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    public void AddGUI(Transform t) {
        guis.Add(t);
        SpriteRenderer parentSr = t.GetComponent<SpriteRenderer>();
        SpriteRenderer[] childrenSr = t.gameObject.GetComponentsInChildren<SpriteRenderer>();

        if (parentSr) ProcessGUI(parentSr);
        if (childrenSr.Length > 0) {
            SortingGroup sg = t.GetComponent<SortingGroup>();
            if(!sg) sg = t.AddComponent<SortingGroup>();
            
            sg.sortingOrder = 0;
            foreach (SpriteRenderer sr in childrenSr) {
                ProcessGUI(sr);
            }
        }
        
        t.transform.rotation = spriteRotation; // only rotate the parent (not the children seperately)
    }

    private void ProcessGUI(SpriteRenderer sr) {
        sr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        sr.receiveShadows = true;
        sr.material = spriteShadowMat;
    }

    public void RemoveGUI(Transform t) => guis.Remove(t);
}
