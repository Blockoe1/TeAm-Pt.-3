/*****************************************************************************
// File Name : DeerViewer.cs
// Author : Arcadia Koederitz
// Creation Date : 10/21/2026
// Last Modified : 10/21/2026
//
// Brief Description : Base class for UI elements that display deer stats.
*****************************************************************************/
using AriUtils;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector2 offset;
    [SerializeField, ComponentReference] private BuckUI ui; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetTooltip(ui.Data);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetTooltip(null);
    }

    private void OnDisable()
    {
        SetTooltip(null);
    }

    private void SetTooltip(DeerData data)
    {
        if (DeerTooltip.Instance != null)
        {
            DeerTooltip.Instance.SetDeer(data, CostParams.Buy);
            DeerTooltip.Instance.MoveTo((Vector2)transform.position + offset);
        }
    }
}
