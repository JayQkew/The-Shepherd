using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
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
        SpriteRenderer sr = t.GetComponent<SpriteRenderer>();
        sr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        sr.receiveShadows = true;
        // sr.material = spriteShadowMat;
        t.transform.rotation = spriteRotation;
    }

    public void RemoveGUI(Transform t) => guis.Remove(t);
}
