using System.Collections.Generic;
using System;
using UnityEngine;

public class BuckScreen : MonoBehaviour
{
    [SerializeField] protected GameObject _screen;
    [SerializeField] private Transform _buckParent;
    [SerializeField] private BuckUI _buckTemplate;
    [SerializeField] private GameObject _blankTemplate;
    protected (DeerData, BuckUI) selected;
    protected int widgetsToKeep = 1;
    protected List<DeerData> currentDeer;

    protected virtual void Start()
    {
        _buckTemplate.gameObject.SetActive(false);
        if (_blankTemplate != null) _blankTemplate.SetActive(false);
    }

    public virtual void BuckSelected(DeerData buck, BuckUI ui) { }

    public void InitializeWidgets(List<DeerData> bucks)
    {
        for (int i = widgetsToKeep; i < _buckParent.childCount; i++)
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
        entry.Data = buck;
    }

    public void ReplaceWithBlank(BuckUI ui)
    {
        var entry = Instantiate(_blankTemplate, _buckParent);
        entry.gameObject.SetActive(true);

        entry.transform.SetSiblingIndex(ui.transform.GetSiblingIndex());
        Destroy(ui.gameObject);
    }

    public virtual void Continue() { }

    public string FormatDoe(int doe)
    {
        string s = doe.ToString();
        if (s.Length == 1) s = "00" + s;
        if (s.Length == 2) s = "0" + s;
        s = s.Substring(0, s.Length - 2) + "." + s.Substring(s.Length - 2);
        return "$" + s;
    }
}
