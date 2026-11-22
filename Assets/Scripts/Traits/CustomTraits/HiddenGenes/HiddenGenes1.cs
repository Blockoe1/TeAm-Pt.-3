using AriUtils;
using UnityEngine;

[DropdownGroup("Hidden Genes")]
[System.Serializable]
public class HiddenGenes1 : Trait
{
    public override Trait GetInheritedTrait()
    {
        return TraitManager.TraitDatabase[TraitManager.GetTraitIndex<HiddenGenes2>()];
    }
}
