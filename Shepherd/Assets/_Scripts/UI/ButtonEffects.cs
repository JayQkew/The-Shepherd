using Audio;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        [SerializeField] private EventReference hover;
        [SerializeField] private EventReference pressed;
        
        public void OnPointerEnter(PointerEventData eventData) {
            AudioManager.Instance.PlayOneShot(hover);
        }

        public void OnPointerDown(PointerEventData eventData) {
            AudioManager.Instance.PlayOneShot(pressed);

        }
    }
}
