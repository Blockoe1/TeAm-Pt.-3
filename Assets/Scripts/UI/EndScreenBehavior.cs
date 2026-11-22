using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenBehavior : MonoBehaviour
{
    public static int RoundsToEnd = 10;

    [SerializeField]
    private TMP_Text _moneyText;

    [SerializeField]
    private TMP_Text _winLosetext;

    [SerializeField]
    private int _roundsToEnd;
    [SerializeField]
    private int _centsToWin = 500;
    [SerializeField]
    private string _winMessage = "You earned $5 BUCKS!";
    [SerializeField]
    private string _loseMessage = "You're broke.";


    private void Start()
    {
        RoundsToEnd = _roundsToEnd;

        _moneyText.text = MoneyManager.FormatCents(MoneyManager.Instance.TotalCents);

        if (MoneyManager.Instance.TotalCents >= _centsToWin) //Win
        {
            _winLosetext.text = _winMessage;
        }
        else //Lose
        {
            _winLosetext.text = _loseMessage;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
