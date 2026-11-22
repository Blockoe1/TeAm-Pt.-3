using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostRaceJunk : MonoBehaviour
{
    [SerializeField] private TMP_Text winLostTXT;
    [SerializeField] private TMP_Text moneyGainLossTXT;

    [SerializeField] private string winText = "You Won!";
    [SerializeField] private string lossText = "Poor Luck!";

    
    [SerializeField]
    private int _ownedWinnerPayout = 50;
    private void Start()
    {
        if (RaceManager.DeerBetOnDeer == RaceManager.WinningDeer)
        {
            Win();
        }
        else
        {
            Lose();
        }
        EndRound();
    }

    private void Win()
    {
        int moneyToGive = Mathf.RoundToInt(MoneyManager.CurrentBet * RaceManager.DeerBetOnDeer.PreviousBetMultiplier() * (1 + (RaceManager.DeerBetOnDeer.Charisma / 100f)));
        winLostTXT.text = winText;
        moneyGainLossTXT.text = "+$" + (moneyToGive / 100f).ToString();
        MoneyManager.Instance.EarnMoney(moneyToGive);
    }

    private void Lose()
    {
        int moneyToLose = MoneyManager.CurrentBet;
        winLostTXT.text = lossText;
        moneyGainLossTXT.text = "-$" + (moneyToLose / 100f).ToString();
    }

    private void EndRound()
    {
        if (PlayerManager.Instance.OwnsDeer(RaceManager.WinningDeer))
            MoneyManager.Instance.EarnMoney(_ownedWinnerPayout);

        DeerPool.Instance.AgeAllDeer();
        PlayerManager.Instance.Round++;
        PlayerManager.Instance.ResetBredOnRound();
    }

    public void ContinueButton()
    {
        if (PlayerManager.Instance.Round >= EndScreenBehavior.RoundsToEnd)
            SceneManager.LoadScene("EndScene");
        else
            SceneManager.LoadScene("PreRaceScene");
    }
}
