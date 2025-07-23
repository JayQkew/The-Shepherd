using System;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    private InputHandler inputHandler;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponentInParent<Rigidbody>();
        inputHandler = GetComponentInParent<InputHandler>();
    }

    private void Update() {
        if (inputHandler.move.x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (inputHandler.move.x < 0) transform.localScale = new Vector3(-1, 1, 1);
    }
}
