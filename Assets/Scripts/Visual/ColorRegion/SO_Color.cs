/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/20/2026 by Marlow Greenan
 * 
 * Contains data for colors and their combinations.
 */
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "C_", menuName = "Scriptable Objects/Color")]
public class SO_Color : ScriptableObject
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private Color _color = new Color(255, 255, 255, 100);
    [SerializeField]
    private Color _shadedColor = new Color(155, 155, 155, 100);

    #region GS
    public string Name { get => _name;}
    public Color Color { get => _color;}
    public Color ShadedColor { get => _shadedColor; }
    #endregion
}