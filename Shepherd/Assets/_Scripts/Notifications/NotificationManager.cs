using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Notifications
{
    public class NotificationManager : MonoBehaviour
    {
        public static NotificationManager Instance { get; private set; }

        [SerializeField] private Transform notificationContainer;
        [SerializeField] private GameObject popUpPrefab;
        [SerializeField] private List<Notification> notifications = new List<Notification>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        private void Update() {
            foreach (Notification n in notifications.ToList()) {
                n.timer.Update();
                if (!n.timer.IsFinished) continue;
                
                n.OnTimerFinished();
                notifications.Remove(n);
            }
        }

        public void ShowPopUp(Notification notification) {
            GameObject popUp = Instantiate(popUpPrefab, notificationContainer);
            NotificationPopUp popupScript = popUp.GetComponent<NotificationPopUp>();
            popupScript.Init(notification);
            notifications.Add(notification);
        }
    }
}
