using System;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private float distance;

    [SerializeField] private Vector2 distanceScaleRatio;
    [SerializeField] private Vector3 maxScale;
    [SerializeField] private Vector3 minScale;

    [SerializeField] private LayerMask castLayer;

    private void FixedUpdate() {
        Cast();
    }

    /// <summary>
    /// shoots a ray down from the follow and will get the yPos and distance
    /// </summary>
    private void Cast() {
        RaycastHit hit;
        Physics.Raycast(transform.parent.position, Vector3.down, out hit, -Mathf.Infinity, castLayer);
        
        distance = hit.distance;
        transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
    }

}
