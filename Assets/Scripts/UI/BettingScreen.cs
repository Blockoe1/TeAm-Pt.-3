using System.Collections.Generic;
using UnityEngine;

public class BettingScreen : MonoBehaviour
{
    [SerializeField] private Transform _buckParent;
    [SerializeField] private BuckUI _buckTemplate;

    private void Start()
    {
        var test = new List<DeerData>();
        for (int i = 0; i < 5; i++)
        {
            test.Add(new DeerData());
            test[^1].Name = (i + 1).ToString();
        }
        InitializeBetting(test);
    }

    public void InitializeBetting(List<DeerData> bucks)
    {
        while (_buckParent.childCount > 1)
        {
            Destroy(_buckParent.GetChild(1).gameObject);
        }

        foreach (var buck in bucks)
        {
            var entry = Instantiate(_buckTemplate, _buckParent).GetComponent<BuckUI>();
            entry.gameObject.SetActive(true);
            entry.BuckName.text = buck.Name;
        }
    }
}
