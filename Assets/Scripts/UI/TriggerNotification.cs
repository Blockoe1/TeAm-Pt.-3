using AriUtils;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerNotification : MonoBehaviour
{
    [SerializeField, BoxGroup("References")] public Image headImage;
    [SerializeField, BoxGroup("References")] public TMP_Text notifyText;
    [SerializeField, ComponentReference] public ColorRegionHandler crh;
    [SerializeField, ComponentReference] public GeneController geneController;
}
