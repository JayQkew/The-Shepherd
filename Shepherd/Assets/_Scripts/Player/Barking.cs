using System;
using System.Collections.Generic;
using UnityEngine;

public class Barking : MonoBehaviour
{
    private InputHandler _inputHandler;

    [SerializeField] private float radius;
    [SerializeField] private float force;
    [SerializeField] private float range;
    [SerializeField] private GameObject[] hitObjects;

    private void Awake() => _inputHandler = GetComponent<InputHandler>();

    public void Bark() {
        hitObjects = ConeCast();
        foreach (GameObject hit in hitObjects) {
            IBarkable b = hit.GetComponent<IBarkable>();
            if (b != null) b.BarkedAt(transform.position);
        }
    }

    private GameObject[] ConeCast() {
        List<GameObject> hits = new List<GameObject>();

        float aimAngle = Mathf.Atan2(_inputHandler.aim.x, _inputHandler.aim.z) * Mathf.Rad2Deg;
        float halfRange = range * 0.5f;
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        Vector3 aimDir = new Vector3(Mathf.Sin(aimAngle * Mathf.Deg2Rad), 0, Mathf.Cos(aimAngle * Mathf.Deg2Rad));
        
        foreach (Collider col in colliders) {
            if (col.gameObject == gameObject) continue;

            Vector3 dir = (col.transform.position - transform.position).normalized;
            dir.y = 0;
            dir.Normalize();
            
            float angle = Vector3.SignedAngle(aimDir, dir, Vector3.up);

            if (angle >= -halfRange && angle <= halfRange) {
                GameObject go = col.gameObject;
                if (!hits.Contains(go)) hits.Add(go);
            }
        }
        return hits.ToArray();
    }

    private void OnDrawGizmos() {
        if (_inputHandler == null) return; // Safety check for editor
        
        float aimAngle = Mathf.Atan2(_inputHandler.aim.x, _inputHandler.aim.z) * Mathf.Rad2Deg;
        float halfRange = range * 0.5f;
        float maxAngle = aimAngle + halfRange;
        float minAngle = aimAngle - halfRange;

        Vector3 aimDir = new Vector3(Mathf.Sin(aimAngle * Mathf.Deg2Rad), 0, Mathf.Cos(aimAngle * Mathf.Deg2Rad));
        Vector3 minDir = new Vector3(Mathf.Sin(minAngle * Mathf.Deg2Rad), 0, Mathf.Cos(minAngle * Mathf.Deg2Rad));
        Vector3 maxDir = new Vector3(Mathf.Sin(maxAngle * Mathf.Deg2Rad), 0, Mathf.Cos(maxAngle * Mathf.Deg2Rad));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + aimDir * radius);
        Gizmos.DrawLine(transform.position, transform.position + minDir * radius);
        Gizmos.DrawLine(transform.position, transform.position + maxDir * radius);
    }
}