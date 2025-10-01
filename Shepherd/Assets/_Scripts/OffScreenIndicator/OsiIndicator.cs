using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace OffScreenIndicator
{
    public class OsiIndicator : MonoBehaviour
    {
        [SerializeField] private OsiTarget target;
        public RectTransform rectTransform;
        [SerializeField] private RectTransform pointer;
        [SerializeField] private TextMeshProUGUI descriptionTxt;
        [SerializeField] private TextMeshProUGUI distanceTxt;

        public void Init(string desc, OsiTarget t) {
            target = t;
            rectTransform = GetComponent<RectTransform>();
            descriptionTxt.text = desc;
        }

        public void Rotate(Vector2 clampedCanvasPos) {
            Vector2 dir = clampedCanvasPos.normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            pointer.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void Distance() {
            distanceTxt.text = target.distance.ToString("0.0") + "m";
        }
    }
}
