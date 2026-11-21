using TMPro;
using UnityEngine;

public class BuckUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _buckName;
    [SerializeField] private RenderTexture _buckAppearance;

    public TMP_Text BuckName { get => _buckName; set => _buckName = value; }
    public RenderTexture BuckAppearance { get => _buckAppearance; set => _buckAppearance = value; }
}
