using System;
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

        public void Init(Notification n) {
            notification = n;
            titleTxt.text = n.title;
            messageTxt.text = n.message;
            
            if (n.icon != null) {
                image.sprite = n.icon;
            }

            n.OnTimerFinished = () => Destroy(gameObject);
        }
    }
}
