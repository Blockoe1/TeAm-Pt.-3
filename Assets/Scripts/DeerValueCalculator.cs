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