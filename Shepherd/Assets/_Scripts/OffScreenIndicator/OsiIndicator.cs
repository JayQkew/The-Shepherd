using TMPro;
using UnityEngine;

namespace OffScreenIndicator
{
    public class OsiIndicator : MonoBehaviour
    {
        public RectTransform rectTransform;
        [SerializeField] private RectTransform pointer;
        [SerializeField] private TextMeshProUGUI text;

        public void Init(string desc) {
            rectTransform = GetComponent<RectTransform>();
            text.text = desc;
        }

        public void Rotate(Vector2 clampedCanvasPos) {
            Vector2 dir = clampedCanvasPos.normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            pointer.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
