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

    private void Awake() {
        _inputHandler = GetComponent<InputHandler>();
    }

    public void Bark() {
        hitObjects = ConeCast();

        foreach (GameObject hit in hitObjects) {
            IBarkable b = hit.GetComponent<IBarkable>();
            if (b != null) {
                b.BarkedAt(transform.position);
            }
        }
    }

    private GameObject[] ConeCast() {
        List<GameObject> hits = new List<GameObject>();

        float aimAngle = Mathf.Atan2(_inputHandler.aim.x, _inputHandler.aim.z) * Mathf.Rad2Deg;
        float halfRange = radius * 0.5f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in colliders) {
            if (col.gameObject == gameObject) continue;

            Vector3 dir = (col.transform.position - transform.position).normalized;
            dir.y = 0;

            Vector3 aimDir = new Vector3(Mathf.Cos(aimAngle * Mathf.Deg2Rad), 0, Mathf.Sin(aimAngle * Mathf.Deg2Rad));
            float colAngle = Vector3.Angle(aimDir, dir);

            if (colAngle <= halfRange) {
                GameObject go = col.gameObject;
                if (!hits.Contains(go)) hits.Add(go);
            }
        }
        // for (int i = 0; i <= numberOfRays; i++) {
        //     float angle = aimAngle - halfRange + i * _gap;
        //     float rad = angle * Mathf.Deg2Rad;
        //     
        //     Vector3 dir = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));
        //     RaycastHit[] rayHits = Array.Empty<RaycastHit>();
        //     int size = Physics.RaycastNonAlloc(transform.position, dir, rayHits, radius);
        //     Debug.Log(size);
        //     foreach (RaycastHit hit in rayHits) {
        //         GameObject go = hit.transform.gameObject;
        //         if (!hits.Contains(go)) {
        //             hits.Add(go);
        //             Debug.Log("Hit: " + go.name);
        //         }
        //     }
        // }

        return hits.ToArray();
    }

    private void OnDrawGizmos() {
        float aimAngle = (_inputHandler == null) ? 0 : Mathf.Atan2(_inputHandler.aim.x, _inputHandler.aim.z);
        float halfRange = range * 0.5f * Mathf.Deg2Rad;

        Vector3 aimDir = new Vector3(Mathf.Sin(aimAngle), 0, Mathf.Cos(aimAngle));
        Vector3 minAimDir = new Vector3(Mathf.Sin(aimAngle - halfRange), 0, Mathf.Cos(aimAngle - halfRange));
        Vector3 maxAimDir = new Vector3(Mathf.Sin(aimAngle + halfRange), 0, Mathf.Cos(aimAngle + halfRange));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + aimDir * radius);
        Gizmos.DrawLine(transform.position, transform.position + minAimDir * radius);
        Gizmos.DrawLine(transform.position, transform.position + maxAimDir * radius);

        // // Draw cone sides
        // Gizmos.color = Color.yellow;
        // for (int i = 0; i <= numberOfRays; i++) {
        //     float angle = aimAngle - halfRange + i * gap;
        //     float rad = angle;
        //     Vector3 dir = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
        //     
        //     Gizmos.DrawLine(transform.position, transform.position + dir * radius);
        // }

        // Draw the arc at the end of the cone
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        int arcResolution = 20;
        float arcStep = range / arcResolution;

        for (int i = 0; i <= arcResolution; i++) {
            float startAngle = aimAngle - halfRange + i * arcStep - 45;
            float endAngle = startAngle + arcStep;

            float startRad = startAngle * Mathf.Deg2Rad;
            float endRad = endAngle * Mathf.Deg2Rad;

            Vector3 startDir = new Vector3(Mathf.Sin(startRad), 0, Mathf.Cos(startRad)) * radius;
            Vector3 endDir = new Vector3(Mathf.Sin(endRad), 0, Mathf.Cos(endRad)) * radius;

            Gizmos.DrawLine(transform.position + startDir, transform.position + endDir);
        }

        // Draw the detection sphere
        Gizmos.color = new Color(1f, 1f, 0f, 0.1f);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}