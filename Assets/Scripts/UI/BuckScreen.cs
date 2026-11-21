using System.Collections.Generic;
using System;
using UnityEngine;

public class BuckScreen : MonoBehaviour
{
    [SerializeField] protected GameObject _screen;
    [SerializeField] private Transform _buckParent;
    [SerializeField] private BuckUI _buckTemplate;
    protected (DeerData, BuckUI) selected;

    protected virtual void Start()
    {
        _buckTemplate.gameObject.SetActive(false);
    }

    public virtual void BuckSelected(DeerData buck, BuckUI ui) { }

    public void InitializeWidgets(List<DeerData> bucks)
    {
        for (int i = 1; i < _buckParent.childCount; i++)
        {
            Destroy(_buckParent.GetChild(i).gameObject);
        }

        _screen.SetActive(true);

        foreach (var buck in bucks)
        {
            InitializeWidget(buck);
        }
    }

    public void InitializeWidget(DeerData buck)
    {
        var entry = Instantiate(_buckTemplate, _buckParent).GetComponent<BuckUI>();
        entry.gameObject.SetActive(true);
        entry.BuckName.text = buck.Name;
        entry.SelectButton.onClick.AddListener(() => BuckSelected(buck, entry));
    }

    public virtual void Continue() { }
}
