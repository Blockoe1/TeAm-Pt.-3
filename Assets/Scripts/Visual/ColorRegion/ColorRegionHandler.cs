/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/20/2026 by Marlow Greenan
 * 
 * Controls the color region material on a sprite renderer.
 */

using MarUtility.ExecutionManagement;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class ColorRegionHandler : Manager
{
    [SerializeField, Tooltip("CASE SENSITIVE\n The IDs for the colors in the color region material.")]
    private string[] _colorIDs;

    //SIMULATION
    [SerializeField, BoxGroup("Simulation")]
    private SO_Color[] _simColors;

    //DATA
    [SerializeField, BoxGroup("Renderer")]
    private ERendererType _renderType;

    private SpriteRenderer[] _spriteRens;
    private Image[] _imageRens;

    public ERendererType RenderType { get => _renderType; }

    public override void Initialize()
    {
        base.Initialize();

        switch (_renderType)
        {
            case ERendererType.SPRITE:
                _spriteRens = GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer sr in _spriteRens)
                    sr.material = new Material(sr.material); break;
            case ERendererType.IMAGE:
                _imageRens = GetComponentsInChildren<Image>();
                foreach (Image i in _imageRens)
                    i.material = new Material(i.material); break;
        }
    }

    public void VisualizeColor(string colorID, string shadedColorID, SO_Color color)
    {
        switch (_renderType)
        {
            case ERendererType.SPRITE:
                foreach (SpriteRenderer sr in _spriteRens)
                {
                    sr.material.SetColor("_" + colorID, color.Color);
                    sr.material.SetColor("_" + shadedColorID, color.Color);
                }

                break;
            case ERendererType.IMAGE:
                foreach (Image i in _imageRens)
                {
                    i.material.SetColor("_" + colorID, color.Color);
                    i.material.SetColor("_" + shadedColorID, color.Color);
                }
                break;
        }
    }

    //private void VisualizeColors(SO_Color[] colors)
    //{
    //    for (int i = 0; i < colors.Length; i++)
    //        VisualizeColor("_" + _colorIDs[i], colors[i]);
    //}

    #region Inspector
    //[Button]
    //private void VisualizeColors()
    //    => VisualizeColors(_simColors);
    #endregion
}

public enum ERendererType
{
    SPRITE = 000,
    IMAGE = 100,
    MESH = 200
}