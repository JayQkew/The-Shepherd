using UnityEngine;
using UnityEngine.UI;

public class OffScreenIndicator : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target; // The object to track
    
    [Header("Indicator Settings")]
    [SerializeField] private RectTransform indicator; // UI element (Image/Sprite)
    [SerializeField] private float edgeBuffer = 50f; // Distance from screen edge
    [SerializeField] private bool rotateIndicator = true; // Should indicator point to target?
    
    [Header("Optional")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera mainCam;
    
    private RectTransform canvasRect;
    
    void Start()
    {
        if (mainCam == null)
            mainCam = Camera.main;
            
        if (canvas == null)
            canvas = FindObjectOfType<Canvas>();
            
        if (canvas != null)
            canvasRect = canvas.GetComponent<RectTransform>();
    }
    
    void Update()
    {
        if (target == null || indicator == null || mainCam == null)
            return;
            
        Vector3 screenPos = mainCam.WorldToViewportPoint(target.position);
        
        // Check if target is behind camera or off screen
        bool isOffScreen = screenPos.z < 0 || screenPos.x < 0 || screenPos.x > 1 || 
                          screenPos.y < 0 || screenPos.y > 1;
        
        indicator.gameObject.SetActive(isOffScreen);
        
        if (isOffScreen)
        {
            // Handle behind camera case
            if (screenPos.z < 0)
            {
                screenPos.x = 1f - screenPos.x;
                screenPos.y = 1f - screenPos.y;
            }
            
            // Clamp to screen bounds
            screenPos.x = Mathf.Clamp(screenPos.x, 0.05f, 0.95f);
            screenPos.y = Mathf.Clamp(screenPos.y, 0.05f, 0.95f);
            
            // Convert to canvas position
            Vector2 canvasPos = new Vector2(
                (screenPos.x - 0.5f) * canvasRect.sizeDelta.x,
                (screenPos.y - 0.5f) * canvasRect.sizeDelta.y
            );
            
            // Clamp to edge with buffer
            float halfWidth = canvasRect.sizeDelta.x / 2f - edgeBuffer;
            float halfHeight = canvasRect.sizeDelta.y / 2f - edgeBuffer;
            
            canvasPos.x = Mathf.Clamp(canvasPos.x, -halfWidth, halfWidth);
            canvasPos.y = Mathf.Clamp(canvasPos.y, -halfHeight, halfHeight);
            
            indicator.anchoredPosition = canvasPos;
            
            // Rotate indicator to point toward target
            if (rotateIndicator)
            {
                Vector2 dir = canvasPos.normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                indicator.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
    
    // Optional: Call this to change the target at runtime
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}