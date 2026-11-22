/*****************************************************************************
// File Name : AgeTrait.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Trait that triggers when a deer ages.
*****************************************************************************/
public class AgeTrigger : TraitTrigger
{
    public int Age { get; set; }

    public AgeTrigger(int age)
    {
        Age = age;
    }
}
