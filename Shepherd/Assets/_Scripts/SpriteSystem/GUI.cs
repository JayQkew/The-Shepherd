using System;
using UnityEngine;

public class GUI : MonoBehaviour
{
    private void Start() {
        SpriteManager.Instance.AddGUI(transform);
    }

    private void OnDestroy() {
        SpriteManager.Instance.RemoveGUI(transform);
    }
}
