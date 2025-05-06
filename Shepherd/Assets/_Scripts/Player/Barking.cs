using System;
using System.Collections.Generic;
using UnityEngine;

public class Barking : MonoBehaviour
{
    private InputHandler _inputHandler;

    [SerializeField] private float radius;
    [SerializeField] private float force;
    [SerializeField] private float range;
    [SerializeField] private int numberOfRays;
    private float _gap; //gap between raycasts

    private void Awake() {
        _gap = range / numberOfRays;
        _inputHandler = GetComponent<InputHandler>();
    }
    
    public void ConeCast() {
        HashSet<GameObject> hits = new HashSet<GameObject>();
        
        float aimAngle = Mathf.Atan2(_inputHandler.aim.x, _inputHandler.aim.z) * Mathf.Rad2Deg;
        float halfRange = radius * 0.5f;
        
        for (int i = 0; i <= numberOfRays; i++) {
            float angle = aimAngle - halfRange + i * _gap;
            float rad = angle * Mathf.Deg2Rad;
            
            Vector3 dir = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));
            RaycastHit[] rayHits = Physics.RaycastAll(transform.position, dir, radius);

            foreach (RaycastHit hit in rayHits) {
                GameObject go = hit.collider.gameObject;
                hits.Add(go);
            }
        }
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
