/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/20/2026 by Marlow Greenan
 * 
 * Controls the color region material on a sprite renderer.
 */

using UnityEngine;
using NaughtyAttributes;
using MarUtility.ExecutionManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorRegionHandler : Manager
{
    [SerializeField, Tooltip("CASE SENSITIVE\n The IDs for the colors in the color region material.")]
    private string[] _colorIDs;

    //SIMULATION
    [SerializeField, BoxGroup("Simulation")]
    private SO_Color[] _simColors;

    //DATA
    private SpriteRenderer spriteRen;

    public override void Initialize()
    {
        base.Initialize();

        spriteRen = GetComponent<SpriteRenderer>();
    }

    public void VisualizeColor(string colorID, SO_Color color)
        => spriteRen.material.SetColor("_" + colorID, color.Color);

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
