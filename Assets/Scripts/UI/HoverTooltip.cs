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
    [SerializeField] private bool buying;
    [SerializeField] private bool betting;
    [SerializeField] private Canvas canvas;
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
            if (buying)
            {
                DeerTooltip.Instance.SetDeer(data, CostParams.Buy);
            }
            else if(betting)
            {
                DeerTooltip.Instance.SetDeer(data, CostParams.Bet);
            }
            else
            {
                DeerTooltip.Instance.SetDeer(data, 0);
            }
            DeerTooltip.Instance.MoveTo((Vector2)transform.position + offset * canvas.scaleFactor);
        }
    }
}
