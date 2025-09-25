using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Notifications
{
    [Serializable]
    public class Notification
    {
        public string title;
        public string message;
        public Sprite icon;
        
        public Notification(string title, string message, Sprite icon) {
            this.title = title;
            this.message = message;
            this.icon = icon;

            if (NotificationManager.Instance != null) {
                NotificationManager.Instance.ShowPopUp(this);
            }
        }

        public Notification(string title, string message) {
            this.title = title;
            this.message = message;
            
            if (NotificationManager.Instance != null) {
                NotificationManager.Instance.ShowPopUp(this);
            }
        }
    }
}
