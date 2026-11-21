/*****************************************************************************
// File Name : BetManager.cs
// Author : Alex Lindsey
// Creation Date : 11/020/2026
// Last Modified : 11/021/2026
//
// Brief Description : Calculates the value of a deer based on its stats, traits, and age for buying and selling purposes.
*****************************************************************************/
using UnityEngine;

public static class DeerValueCalculator
{
    public static float CalculateBaseValue(int totalStats, int goodTraitCount)
    {
        return totalStats * ((goodTraitCount + 1f) / 2f);
    }

    public static float CalculatePrice(int totalStats, int goodTraitCount, bool breederAge, bool retired)
    {
        float value = CalculateBaseValue(totalStats, goodTraitCount);

        if (breederAge)
            value *= 1.25f;

        if (retired)
            value *= 0.5f;

        return value;
    }

    public static float CalculateSellPrice(int totalStats, int goodTraitCount, bool breederAge, bool retired)
    {
        float value = CalculateBaseValue(totalStats, goodTraitCount);

        if (breederAge)
            value *= 1.25f;

        if (retired)
            value *= 0.5f;

        return value;
    }
}