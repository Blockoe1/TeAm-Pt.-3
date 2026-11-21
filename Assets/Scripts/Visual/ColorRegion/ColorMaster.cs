/*
 * Marlow Greenan
 * Created: 11/20/2026
 * Last Updated: 11/20/2026 by Marlow Greenan
 * 
 * Controls the color region material on a sprite renderer.
 */
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class ColorMaster : MonoBehaviour
{
    private static ColorMaster inst;

    [SerializeField, Tooltip("Key is resulting color.\nValue is combination to get color.")]
    private Dictionary<SO_Color, ColorCombo> _colorCombos;

    //SIMULATION
    [SerializeField, BoxGroup("Simulation")]
    private SO_Color _simColorA;
    [SerializeField, BoxGroup("Simulation")]
    private SO_Color _simColorB;

    public static ColorMaster INST { get => inst; }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (inst == null)
            inst = this;
        else
        {
            Debug.LogError("Multiple copies of ColorMaster exists, please delete one.");
            Destroy(this);
        }
    }

    public SO_Color Combine(SO_Color colorA, SO_Color colorB)
    {
        foreach (KeyValuePair<SO_Color, ColorCombo> kvp in _colorCombos)
        {
            if (kvp.Value.ColorA == colorA && kvp.Value.ColorB == colorB)
                return kvp.Key;
            if (kvp.Value.ColorA == colorB && kvp.Value.ColorB == colorA)
                return kvp.Key;
        }
        Debug.LogError($"Color combination {colorA.name} + {colorB.name} does not exist in color master.");
        return null;
    }

    #region Inspector
    [Button]
    private void Combine()
        => Debug.Log(Combine(_simColorA, _simColorB).Name);
    #endregion

    [System.Serializable]
    private struct ColorCombo
    {
        [SerializeField]
        private SO_Color _colorA;

        [SerializeField]
        private SO_Color _colorB;

        #region GS
        public SO_Color ColorA { get => _colorA; }
        public SO_Color ColorB { get => _colorB; }
        #endregion
    }
}
