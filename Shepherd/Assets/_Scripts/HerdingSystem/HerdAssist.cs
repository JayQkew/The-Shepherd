using System;
using UnityEngine;

public class HerdAssist : MonoBehaviour
{
    [SerializeField] private HerdDirection direction;
    [SerializeField] private float centerMult;
    [SerializeField] private float pushMult;

    private void PushDirection(Rigidbody rb, Vector3 dir, float forceMult) {
        Vector3 localForce = dir.normalized;
        Vector3 worldForce = transform.TransformDirection(localForce);
        rb.AddForce(worldForce * forceMult, ForceMode.Impulse);
    }

    private void OnTriggerStay(Collider other) {
        HerdAnimal h = other.gameObject.GetComponent<HerdAnimal>();
        if (h != null && direction != HerdDirection.None) {
            Vector3 localPos = transform.InverseTransformPoint(h.transform.position);
            PushDirection(h.rb, new Vector3(-localPos.x, 0, 0), centerMult);
            if(direction == HerdDirection.In) PushDirection(h.rb, -transform.forward, pushMult);
            else PushDirection(h.rb, transform.forward, pushMult);
        }
    }

    public enum HerdDirection
    {
        None,
        In,
        Out
    }
}
