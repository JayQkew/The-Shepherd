using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Barking : MonoBehaviour
{
    private InputHandler _inputHandler;

    [SerializeField] private float radius;
    [SerializeField] private float force;
    [SerializeField] private float range;
    [SerializeField] private int numberOfRays;
    private float _gap; //gap between raycasts
    [SerializeField] private GameObject[] hitObjects;

    private void Awake() {
        _gap = range / numberOfRays;
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Update() {
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
        
        for (int i = 0; i <= numberOfRays; i++) {
            float angle = aimAngle - halfRange + i * _gap;
            float rad = angle * Mathf.Deg2Rad;
            
            Vector3 dir = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));
            RaycastHit[] rayHits = Array.Empty<RaycastHit>();
            int size = Physics.RaycastNonAlloc(transform.position, dir, rayHits, radius);
            Debug.Log(size);
            foreach (RaycastHit hit in rayHits) {
                GameObject go = hit.transform.gameObject;
                if (!hits.Contains(go)) {
                    hits.Add(go);
                    Debug.Log("Hit: " + go.name);
                }
            }
        }

        return hits.ToArray();
    }

    private void OnDrawGizmos() {
        float aimAngle = Mathf.Atan2(_inputHandler.aim.x, _inputHandler.aim.z);
        float gap = _gap > 0 ? _gap : range / numberOfRays;
        Gizmos.color = Color.yellow;
        for (int i = 0; i <= numberOfRays; i++) {
            float angle = i * gap * Mathf.Deg2Rad;
            angle += range * 0.5f * Mathf.Deg2Rad;
            angle -= aimAngle;
            Vector3 dir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            
            Gizmos.DrawLine(transform.position, transform.position + dir * radius);
        }
    }
    
}
