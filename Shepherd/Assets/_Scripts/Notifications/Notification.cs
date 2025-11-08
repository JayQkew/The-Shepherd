using System;
using Audio;
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

        public Timer timer;

        public Action OnTimerFinished;
        
        public Notification(string title, string message, float duration, Sprite icon) {
            this.title = title;
            this.message = message;
            this.icon = icon;
            
            timer.maxTime = duration;

            if (NotificationManager.Instance != null) {
                NotificationManager.Instance.ShowPopUp(this);
            }
        }

        public Notification(string title, string message, float duration) {
            this.title = title;
            this.message = message;
            
            timer.maxTime = duration;
            
            if (NotificationManager.Instance != null) {
                NotificationManager.Instance.ShowPopUp(this);
            }
        }
    }
}
