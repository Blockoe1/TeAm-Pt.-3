using UnityEngine;

public class BetManager : MonoBehaviour
{

    //public int CalculateBettingPoints(DeerController deer)
    //{
    //    int points = deer.TotalStats;

    //    points += deer.GoodTraitCount * 20;
    //    points += deer.SpecialTraitCount * 10;

    //    points += deer.Wins * 25;
    //    points -= deer.Losses * 5;

    //    return Mathf.Clamp(points, 0, 160);
    //}

    //public float GetBaseOdds(DeerController deer)
    //{
    //    int points = CalculateBettingPoints(deer);

    //    if (points >= 130) return 1.25f;
    //    if (points >= 110) return 1.5f;
    //    if (points >= 90) return 2.0f;
    //    if (points >= 70) return 2.5f;
    //    if (points >= 50) return 3.0f;
    //    if (points >= 30) return 3.5f;
    //    if (points >= 10) return 4.0f;

    //    return 5.0f;
    //}

    //public float GetCharismaMultiplier(DeerController deer)
    //{
    //    return 1f + (deer.Charisma / 100f);
    //}

    //public float GetFinalOdds(DeerController deer)
    //{
    //    return GetBaseOdds(deer) * GetCharismaMultiplier(deer);
    //}

    //public string GetOddsDisplay(DeerController deer)
    //{
    //    return $"1:{GetFinalOdds(deer):0.##}";
    //}

    //public float CalculatePayout(DeerController deer, float betAmount)
    //{
    //    return betAmount * GetFinalOdds(deer);
    //}
}