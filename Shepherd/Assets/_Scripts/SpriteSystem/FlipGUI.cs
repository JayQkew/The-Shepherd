using System;
using UnityEngine;

public class FlipGUI : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float flipThresh;

    private Vector3 prevVel;

    private void FixedUpdate() {
        FlipCheck();
    }

    private void FlipCheck() {
        float xVel = rb.linearVelocity.x;
        if(prevVel.x > -flipThresh && xVel <= -flipThresh) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        if(prevVel.x < flipThresh && xVel >= flipThresh) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        
        prevVel = rb.linearVelocity;
    }
}
