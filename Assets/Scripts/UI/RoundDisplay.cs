using AriUtils;
using TMPro;
using UnityEngine;

public class RoundDisplay : MonoBehaviour
{
    [SerializeField, ComponentReference] private TMP_Text moneyText;

    void Start()
    {
        moneyText.text = "Round: " + PlayerManager.Instance.Round.ToString();
    }
}
