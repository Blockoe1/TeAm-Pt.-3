/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/20/2026 by Marlow Greenan
 * 
 * Controls the color region material on a sprite renderer.
 */

using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorRegionHandler : MonoBehaviour
{
    [SerializeField, Tooltip("CASE SENSITIVE\n The IDs for the colors in the color region material.")]
    private string[] _colorIDs;

    //SIMULATION
    [SerializeField]
    private SO_Color[] _simColors;

    //DATA
    [SerializeField, ReadOnly]
    private SpriteRenderer spriteRen;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        spriteRen = GetComponent<SpriteRenderer>();
    }

    private void VisualizeColors(SO_Color[] colors)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            spriteRen.material.SetColor("_" + _colorIDs[i], colors[i].Color);
        }
    }

    #region Inspector
    [Button]
    private void VisualizeColors()
        => VisualizeColors(_simColors);
    #endregion
}
