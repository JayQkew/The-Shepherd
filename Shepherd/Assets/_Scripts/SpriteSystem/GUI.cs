using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GUI : MonoBehaviour
{
    private void Start() {
        SpriteManager.Instance.AddGUI(transform);
    }

    private void OnDestroy() {
        SpriteManager.Instance.RemoveGUI(transform);
    }
}
