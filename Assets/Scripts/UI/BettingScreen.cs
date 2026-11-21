using System.Collections.Generic;
using UnityEngine;

public class BettingScreen : MonoBehaviour
{
    [SerializeField] private Transform _buckParent;
    [SerializeField] private BuckUI _buckTemplate;

    private void Start()
    {
        InitializeBetting(new List<string>() { "A", "B", "C", "D", "E" });
    }

    public void InitializeBetting(List<string> bucks)
    {
        while (_buckParent.childCount > 1)
        {
            Destroy(_buckParent.GetChild(1).gameObject);
        }

        foreach (var buck in bucks)
        {
            var entry = Instantiate(_buckTemplate, _buckParent).GetComponent<BuckUI>();
            entry.gameObject.SetActive(true);
            entry.BuckName.text = buck;
        }
    }
}
