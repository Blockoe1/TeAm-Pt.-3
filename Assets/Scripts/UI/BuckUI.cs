using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuckUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _buckName;
    [SerializeField] private RenderTexture _buckAppearance;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Image _background;

    public TMP_Text BuckName { get => _buckName; set => _buckName = value; }
    public RenderTexture BuckAppearance { get => _buckAppearance; set => _buckAppearance = value; }
    public Button SelectButton { get => _selectButton; set => _selectButton = value; }
    public Image Background { get => _background; set => _background = value; }

    public DeerData Data { get; set; }
}