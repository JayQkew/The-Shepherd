using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Notifications
{
    public class NotificationPopUp : MonoBehaviour
    {
        public Notification notification;
        [Space(25)]
        [SerializeField] private TextMeshProUGUI titleTxt;
        [SerializeField] private TextMeshProUGUI messageTxt;
        [SerializeField] private Image image;

        public void Init(Notification notif) {
            titleTxt.text = notif.title;
            messageTxt.text = notif.message;
            
            if (notif.icon != null) {
                image.sprite = notif.icon;
            }
        }
    }
}
