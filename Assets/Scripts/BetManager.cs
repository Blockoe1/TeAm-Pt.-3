using UnityEngine;

public class BetManager : MonoBehaviour
{
    private float []basePayouts;
    private float []payouts;
    private float betAmount;
    private float charismaChance;

    public float payout(int index, float betAmount)
    {
        if (index < 0 || index >= payouts.Length)
        {
            Debug.LogError("Invalid payout index.");
            return betAmount;
        }
        float payoutAmount = payouts[index] * betAmount;
        return payoutAmount;
    }

    private void calculatePayouts(float []winLossRatio, float []charisma)
    {
        for (int i = 0; i < basePayouts.Length; i++)
        {
            float charismaMult = charismaCheck(charisma[i]);
            payouts[i] = basePayouts[i] * winLossRatio[i] * charismaMult;
        }

    }

    private float charismaCheck(float charisma)
    {
        float check = Random.Range(0 + charisma, 100);
        if (check > charismaChance)
        {
            float mult = charisma / 100 + 1;
            return mult;
        }
        else
        {
            return 1;
        }

    }
}
