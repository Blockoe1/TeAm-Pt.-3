using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BettingScreen : BuckScreen
{
    [SerializeField] private RaceSelectionScreen _selectionScreen;
    [SerializeField] private TMP_Text _betText;
    [SerializeField] private int _defaultBet;
    [SerializeField] private int _betIncrement;
    private int bet;

    private List<DeerData> deerGoingToRace;

    protected override void Start()
    {
        base.Start();
        _screen.SetActive(false);
        deerGoingToRace = new();

        
    }

    /// <summary>
    /// Run when you hit continue on selection screen
    /// </summary>
    public void InitializeBetting(DeerData deer)
    {
        deerGoingToRace = DeerPool.Instance.GetDeerFromPools(DeerPool.PoolType.Mid, DeerPool.PoolType.Mid, DeerPool.PoolType.Bad, DeerPool.PoolType.Good);
        // Fetch relevant deer for the race
        List<DeerData> bucks = new List<DeerData>();
        //for (int i = 0; i < 5; i++)
        //{
        //    bucks.Add(DeerData.NewRandomDeer());
        //    //bucks[^1].Data = new DeerData();
        //    //bucks[^1].Data.Name = (i + 1).ToString();
        //}
        int playerPos = Random.Range(0, 5);
        foreach (DeerData enemyDeer in deerGoingToRace)
        {
            bucks.Add(enemyDeer);
        }

        bucks.Insert(playerPos, deer);

        RaceManager.DeerInRace = bucks.ToArray();


        selected = default;
        InitializeWidgets(bucks);

        bet = _defaultBet;
        _betText.text = FormatDoe(bet);
    }

    public override void BuckSelected(DeerData buck, BuckUI ui)
    {
        if (selected != default)
        {
            selected.Item2.Background.color = Color.wheat;
        }

        selected = (buck, ui);
        CheckButtonDisabled();
        ui.Background.color = Color.orange;
    }

    public override void Continue()
    {
        _screen.SetActive(false);

        // TODO: store bet
        RaceManager.DeerBetOn = selected.Item2.transform.GetSiblingIndex() - widgetsToKeep;
        MoneyManager.CurrentBet = bet;
        MoneyManager.Instance.EarnMoney(-MoneyManager.CurrentBet);
        // TODO: start race
        SceneManager.LoadScene("RaceScene");
    }

    public void BetUp()
    {
        if(MoneyManager.Instance.CanAfford(bet + _betIncrement))
        {
            bet += _betIncrement;
            _betText.text = FormatDoe(bet);
        }
    }

    public void BetDown()
    {
        if (bet - _betIncrement <= 0) return;
        bet -= _betIncrement;
        _betText.text = FormatDoe(bet);
    }

    public void Back()
    {
        _screen.SetActive(false);

        _selectionScreen.InitializeSelection();
    }
}
