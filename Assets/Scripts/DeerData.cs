/*****************************************************************************
// File Name : DeerData.cs
// Author : Arcadia Koederitz
// Creation Date : 10/04/2026
// Last Modified : 10/04/2026
//
// Brief Description : Holds data related to a deer.
*****************************************************************************/
using AriUtils;
using UnityEngine;

[System.Serializable]
public class DeerData
{
    [field: SerializeField] public string Name { get; set; }
    #region Stats
    [field: SerializeField, Range(1, 20)] public int Speed { get; set; } = 1;
    [field: SerializeField, Range(1, 20)] public int Acceleration { get; set; } = 1;
    [field: SerializeField, Range(1, 20)] public int Stamina { get; set; } = 1;
    [field: SerializeField, Range(1, 20)] public int Balance { get; set; } = 1;
    [field: SerializeField, Range(1, 20)] public int Discipline { get; set; } = 1;
    [field: SerializeField, Range(1, 20)] public int Charisma { get; set; } = 1;
    #endregion

    [field: SerializeField, Min(0)] public int Age { get; set; }

    [field: SerializeReference, ClassDropdown] public Trait Trait1 { get; private set; }
    [field: SerializeReference, ClassDropdown] public Trait Trait2 { get; private set; }



    public int Wins { get; private set; }
    public int Races { get; private set; }



    public void LogRace(bool wasWin)
    {
        Races++;
        if (wasWin)
        {
            Wins++;
        }
    }

    public void SetStat(StatType stat, int value)
    {
        switch (stat)
        {
            case StatType.Speed:
                Speed = value; break;
            case StatType.Acceleration:
                Acceleration = value; break;
            case StatType.Stamina:
                Stamina = value; break;
            case StatType.Balance:
                Balance = value; break;
            case StatType.Discipline:
                Discipline = value; break;
            case StatType.Charisma:
                Charisma = value; break;
        }
    }

    public int GetStats(StatType stat)
    {
        switch(stat)
        {
            case StatType.Speed:
                return Speed;
            case StatType.Acceleration:
                return Acceleration;
            case StatType.Stamina:
                return Stamina;
            case StatType.Balance:
                return Balance;
            case StatType.Discipline:
                return Discipline;
            case StatType.Charisma:
                return Charisma;
            default:
                return 0;
        }
    }
}
