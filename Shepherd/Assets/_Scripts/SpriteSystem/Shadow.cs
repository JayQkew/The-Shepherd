using System;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float distance;

    [SerializeField] private Vector2 distanceScaleRatio;
    [SerializeField] private Vector3 maxScale;
    [SerializeField] private Vector3 minScale;

    [SerializeField] private LayerMask castLayer;

    private void Start() {
        maxScale = transform.localScale;
    }

    private void FixedUpdate() {
        Cast();
        Size();
    }

    /// <summary>
    /// shoots a ray down from the follow and will get the yPos and distance
    /// </summary>
    private void Cast() {
        RaycastHit hit;
        Vector3 rayOrigin = transform.parent.position;
        
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, Mathf.Infinity, castLayer)) {
            distance = hit.distance;
            
            // Use parent position as base, then add offset
            Vector3 shadowPosition = new Vector3(
                rayOrigin.x + offset.x,
                hit.point.y + offset.y,
                rayOrigin.z + offset.z
            );
            
            transform.position = shadowPosition;
        }
    }

    private void Size() {
        float clampedDistance = Mathf.Clamp(distance, distanceScaleRatio.x, distanceScaleRatio.y);
        float t = (clampedDistance - distanceScaleRatio.x) / (distanceScaleRatio.y - distanceScaleRatio.x);
        Vector3 targetScale = Vector3.Lerp(maxScale, minScale, t);
        transform.localScale = targetScale;
    }

}
