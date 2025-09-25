using UnityEngine;

namespace UI
{
    public class UIFunctionality : MonoBehaviour
    {
        public void ToggleUI() {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
