using AriUtils;
using UnityEngine;

[DropdownGroup("Hidden Genes")]
public class HiddenGenes2 : Trait
{
    public override int GetInheritedTraitIndex()
    {
        return TraitManager.GetTraitIndex<Champion>();
    }
}
