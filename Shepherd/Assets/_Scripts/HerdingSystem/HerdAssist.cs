using System;
using UnityEngine;

public class HerdAssist : MonoBehaviour
{
    [SerializeField] private float assistMult;

    private void OnTriggerStay(Collider other) {
        HerdAnimal h = other.gameObject.GetComponent<HerdAnimal>();
        if (h != null) {
            Vector3 localPos = transform.InverseTransformPoint(h.transform.position);
            float xDir = -localPos.x;
            Vector3 localForce = new Vector3(xDir, 0, 0).normalized;
            Vector3 worldForce = transform.TransformDirection(localForce);
            
            h.rb.AddForce(worldForce * assistMult, ForceMode.Impulse);
        }
    }
}
