using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Notifications
{
    public class NotificationManager : MonoBehaviour
    {
        public static NotificationManager Instance { get; private set; }

        [SerializeField] private Transform notificationContainer;
        [SerializeField] private GameObject popUpPrefab;
        public static Queue<Notification> Notifications = new Queue<Notification>();

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        public void ShowPopUp(Notification notification) {
            GameObject popUp = Instantiate(popUpPrefab, notificationContainer);
            NotificationPopUp popupScript = popUp.GetComponent<NotificationPopUp>();
            popupScript.notification = notification;
        }
    }
}
