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

    [SerializeField, ShowIf("_renderType", ERendererType.SPRITE), BoxGroup("Renderer")]
    private SpriteRenderer _spriteRen;

    [SerializeField, ShowIf("_renderType", ERendererType.IMAGE), BoxGroup("Renderer")]
    private Image _imageRen;

    public override void Initialize()
    {
        base.Initialize();

        switch (_renderType)
        {
            case ERendererType.SPRITE: _spriteRen.material = new Material(_spriteRen.material); break;
            case ERendererType.IMAGE: _imageRen.material = new Material(_imageRen.material); break;
        }
    }

    public void VisualizeColor(string colorID, SO_Color color)
    {
        switch (_renderType)
        {
            case ERendererType.SPRITE: _spriteRen.material.SetColor("_" + colorID, color.Color); break;
            case ERendererType.IMAGE: _imageRen.material.SetColor("_" + colorID, color.Color); break;
        }
    }

    private void VisualizeColors(SO_Color[] colors)
    {
        for (int i = 0; i < colors.Length; i++)
            VisualizeColor("_" + _colorIDs[i], colors[i]);
    }

    #region Inspector
    [Button]
    private void VisualizeColors()
        => VisualizeColors(_simColors);
    #endregion
}

public enum ERendererType
{
    SPRITE = 000,
    IMAGE = 100,
    MESH = 200
}