using System.Linq;
using UnityEngine;

[System.Serializable]
public class Mutation : Trait
{
    public override Trait GetInheritedTrait()
    {
        return TraitManager.GetRandomTraits(new TraitAlignment[] { Alignment }, 1).FirstOrDefault();
    }
}
