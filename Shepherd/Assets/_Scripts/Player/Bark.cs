using System;
using UnityEngine;

public class Bark : MonoBehaviour
{
    private InputHandler _inputHandler;

    [SerializeField] private float radius;
    [SerializeField] private float force;
    [SerializeField] private float range;
    [SerializeField] private int numberOfRays;
    private float _gap; //gap between raycasts

    private void Awake() {
        float gap = range / numberOfRays;
        _inputHandler = GetComponent<InputHandler>();
    }

    private void ConeCast() {
        for (int i = 0; i <= numberOfRays; i++) {
            float angle = i * _gap * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Physics.RaycastAll(transform.position, dir, radius);
        }
    }

    private void OnDrawGizmos() {
        float gap = _gap > 0 ? _gap : range / numberOfRays;
        Gizmos.color = Color.yellow;
        for (int i = 0; i <= numberOfRays; i++) {
            float angle = i * gap * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            
            Gizmos.DrawLine(transform.position, transform.position + dir * radius);
        }
        
        Gizmos.color = Color.yellow;
        DrawArc(transform.position, radius, 0, range);
    }
    
    // Helper method to draw an arc in the XZ plane
    private void DrawArc(Vector3 center, float radius, float startAngle, float endAngle, int segments = 50) {
        float angleStep = endAngle / segments;
        
        for (int i = 0; i < segments; i++) {
            float angle1 = startAngle + i * angleStep * Mathf.Deg2Rad;
            float angle2 = startAngle + (i + 1) * angleStep * Mathf.Deg2Rad;
            
            Vector3 point1 = center + new Vector3(Mathf.Cos(angle1), 0, Mathf.Sin(angle1)) * radius;
            Vector3 point2 = center + new Vector3(Mathf.Cos(angle2), 0, Mathf.Sin(angle2)) * radius;
            
            Gizmos.DrawLine(point1, point2);
        }
    }
}
