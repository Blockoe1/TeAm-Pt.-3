using UnityEngine;

[CreateAssetMenu(fileName = "AlleleC_", menuName = "Scriptable Objects/Genetics/Color Allele")]
public class SO_ColorAllele : SO_Allele
{
    [SerializeField]
    private SO_Color _color;

    #region
    public SO_Color Color { get => _color; }
    #endregion
}
