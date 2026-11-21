/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/20/2026 by Marlow Greenan
 * 
 * Controls the color region material on a sprite renderer.
 */
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorRegionHandler : MonoBehaviour
{
    [SerializeField, Tooltip("CASE SENSITIVE\n The IDs for the colors in the color region material.")]
    private string[] _colorIDs;

    private SpriteRenderer spriteRen;


    private void Initialize()
    {
        spriteRen = GetComponent<SpriteRenderer>();
    }

    private void VisualizeColors(Color[] colors)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            //Skip id that is not present in color region material.
            if (!spriteRen.material.HasColor(_colorIDs[i]))
            {
                Debug.LogError($"Material {spriteRen.material.name} on {gameObject.name} does not have a color variable \"{_colorIDs[i]}\".");
                continue;
            }

            spriteRen.material.SetColor(_colorIDs[i], colors[i]);
        }
    }
}
