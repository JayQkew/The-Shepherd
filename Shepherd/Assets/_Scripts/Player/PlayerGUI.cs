using System;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    private InputHandler inputHandler;
    private Animator anim;

    private void Start() {
        inputHandler = GetComponentInParent<InputHandler>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if (inputHandler.move.x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (inputHandler.move.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        
        if (inputHandler.move != Vector3.zero && inputHandler.isSprinting) anim.SetInteger("Move", 2);
        else if (inputHandler.move != Vector3.zero) anim.SetInteger("Move", 1);
        else anim.SetInteger("Move", 0);
    }
}
