using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Notifications
{
    public class NotificationPopUp : MonoBehaviour
    {
        [SerializeField] private Notification notification;
        [Space(25)]
        [SerializeField] private TextMeshProUGUI titleTxt;
        [SerializeField] private TextMeshProUGUI messageTxt;
        [SerializeField] private Image image;

        public void Init(Notification notif) {
            notification = notif;
            titleTxt.text = notif.title;
            messageTxt.text = notif.message;
            
            if (notif.icon != null) {
                image.sprite = notif.icon;
            }
        }
    }
}
