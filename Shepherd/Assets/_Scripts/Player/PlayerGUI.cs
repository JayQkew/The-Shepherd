using System;
using Player;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    private InputHandler inputHandler;
    private Animator anim;
    private Movement movement;

    private void Start() {
        inputHandler = GetComponentInParent<InputHandler>();
        movement = GetComponentInParent<Movement>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        Flip();
        Movement();
        
        anim.SetBool("Grounded", movement.grounded);

    }

    private void Flip() {
        if (inputHandler.move.x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (inputHandler.move.x < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void Movement() {
        if (inputHandler.move != Vector3.zero && inputHandler.isSprinting) anim.SetInteger("Move", 2);
        else if (inputHandler.move != Vector3.zero) anim.SetInteger("Move", 1);
        else anim.SetInteger("Move", 0);
    }
}
