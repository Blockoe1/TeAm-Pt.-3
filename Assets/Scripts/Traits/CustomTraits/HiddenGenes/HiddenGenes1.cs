using AriUtils;
using UnityEngine;

[DropdownGroup("Hidden Genes")]
public class HiddenGenes1 : Trait
{
    public override int GetInheritedTraitIndex()
    {
        return TraitManager.GetTraitIndex<HiddenGenes2>();
    }
}
