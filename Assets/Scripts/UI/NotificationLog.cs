using AriUtils;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationLog : MonoBehaviour
{
    [SerializeField]
    private Transform _notificationHolder;

    [SerializeField]
    private ObjectPool<TriggerNotification> _notifcationPool;
    [SerializeField] private SO_PartGene headGene;

    private static Queue<ActNotifcation> notificationQueue = new Queue<ActNotifcation>();

    private void Start()
    {
        NotifyAll();
    }

    public void NotifyAll()
    {
        while(notificationQueue.Count > 0)
            Notify(notificationQueue.Dequeue());
        notificationQueue.Clear();
    }

    private void Notify(ActNotifcation actN)
    {
        if (PlayerManager.Instance.OwnedDeer.ContainsKey(actN.deerData))
        {
            TriggerNotification notifyObj = _notifcationPool.GetObject();
            notifyObj.notifyText.text = ($"{actN.deerData.Name}: {actN.message}");

            // Sprite Head
            notifyObj.geneController.VisualizePartGenome(actN.deerData, headGene);
            notifyObj.geneController.VisualizeColorGenotype(actN.deerData);
        }
    }

    public static void QueueNotification(string message, DeerData data)
    {
        notificationQueue.Enqueue(new ActNotifcation(message, data));
    }

    private struct ActNotifcation
    {
        internal string message;
        internal DeerData deerData;

        public ActNotifcation(string message, DeerData data)
        {
            this.message = message;
            deerData = data;
        }
    }
}
