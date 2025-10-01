using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace OffScreenIndicator
{
    public class OsiManager : MonoBehaviour
    {
        public static OsiManager Instance { get; private set; }

        [SerializeField] private List<Transform> targets;
        private List<OsiIndicator> indicators;
        private Dictionary<OsiTarget, OsiIndicator> targetIndicators = new Dictionary<OsiTarget, OsiIndicator>();
        [Space(10)]
        [SerializeField] private float margin;
        [Space(10)]
        [SerializeField] private Canvas canvas;
        [SerializeField] private Camera mainCam;
        [SerializeField] private GameObject indicatorPrefab;
        
        private RectTransform canvasRect;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        private void Start() {
            if (mainCam == null) mainCam = Camera.main;
            if (canvas == null) Debug.LogError("Off Screen Indicator is missing a Canvas");
            if (canvas != null) canvasRect = canvas.GetComponent<RectTransform>();
        }

        private void Update() {
            if (targets.Count == 0 || mainCam == null) return;

            foreach (OsiTarget target in targetIndicators.Keys) {
                FollowTarget(target);
            }
        }

        /// <summary>
        /// Adding a target for an off-screen indicator
        /// </summary>
        public void AddTarget(Transform target) {
            targets.Add(target);
            
            GameObject newIndicator = Instantiate(indicatorPrefab, transform);
            OsiIndicator indicator = newIndicator.GetComponent<OsiIndicator>();
            
            OsiTarget osiTarget = target.GetComponent<OsiTarget>();
            if (osiTarget == null) {
                osiTarget = target.AddComponent<OsiTarget>();
            }
            
            indicator.Init(osiTarget.description);
            targetIndicators.TryAdd(osiTarget, indicator);
        }

        public void AddTarget(OsiTarget target) {
            targets.Add(target.transform);
            
            GameObject newIndicator = Instantiate(indicatorPrefab, transform);
            OsiIndicator indicator = newIndicator.GetComponent<OsiIndicator>();
            indicator.Init(target.description);
            
            targetIndicators.TryAdd(target, indicator);
        }

        public void FollowTarget(OsiTarget target) {
            Vector3 screenPos = mainCam.WorldToViewportPoint(target.transform.position);
            
            bool isOffScreen = screenPos.z < 0 || 
                               screenPos.x < 0 || screenPos.x > 1 ||
                               screenPos.y < 0 || screenPos.y > 1;
            
            targetIndicators[target].gameObject.SetActive(isOffScreen);

            if (isOffScreen) {
                if (screenPos.z < 0) {
                    screenPos.x = 1f - screenPos.x;
                    screenPos.y = 1f - screenPos.y;
                }
                
                screenPos.x = Mathf.Clamp(screenPos.x, 0, 1);
                screenPos.y = Mathf.Clamp(screenPos.y, 0, 1);
                
                Vector2 canvasPos = new Vector2(
                    (screenPos.x - 0.5f) * canvasRect.sizeDelta.x,
                    (screenPos.y - 0.5f) * canvasRect.sizeDelta.y);
                
                float halfWidth = canvasRect.sizeDelta.x / 2f - margin;
                float halfHeight = canvasRect.sizeDelta.y / 2f - margin;
                
                canvasPos.x = Mathf.Clamp(canvasPos.x, -halfWidth, halfWidth);
                canvasPos.y = Mathf.Clamp(canvasPos.y, -halfHeight, halfHeight);
                
                targetIndicators[target].rectTransform.anchoredPosition = canvasPos;
                targetIndicators[target].Rotate(canvasPos);
            }
            
        }
    }
}
