using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationLog : MonoBehaviour
{
    [SerializeField]
    private Transform _notificationHolder;

    [SerializeField]
    private GameObject _notifcationPrefab;

    public static List<ActNotifcation> Notifications = new List<ActNotifcation>();

    private void Start()
    {
        EmptyNotifyQueue();
    }

    public void EmptyNotifyQueue()
    {
        foreach (ActNotifcation actN in Notifications)
            Notify(actN);
        Notifications.Clear();
    }

    public void Notify(ActNotifcation actN)
    {
        GameObject notifyGO = Instantiate(_notifcationPrefab, _notificationHolder);
        TMP_Text notifyText = notifyGO.GetComponentInChildren<TMP_Text>();
        notifyText.color = actN.Notification.TextColor;
        notifyText.text = ($"{actN.DeerData.Name} {actN.Notification.Message}");
    }

    public void QueueNotification(ActNotifcation actN)
    {
        Notifications.Add(actN);
    }

    public class ActNotifcation
    {
        private Notifcation notification;
        private DeerData deerData;

        public ActNotifcation(Notifcation n, DeerData d)
        {
            notification = n;
            deerData = d;
        }

        #region GS
        public Notifcation Notification { get => notification; }
        public DeerData DeerData { get => deerData; }
        #endregion
    }
}

[System.Serializable]
public class Notifcation
{
    [SerializeField]
    private string _message = "empty";
    [SerializeField]
    private Color _textColor = Color.white;

    #region GS
    public string Message { get => _message; }
    public Color TextColor { get => _textColor; }
    #endregion
}
