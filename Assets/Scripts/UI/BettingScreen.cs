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
    [SerializeField] private BuckUI _preview;
    [SerializeField] private GameObject _emptyPreview;
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
        _emptyPreview.SetActive(true);
        _preview.gameObject.SetActive(false);

        bet = _defaultBet;
        _betText.text = FormatDoe(bet);

        CheckButtonDisabled();
    }

    public override void BuckSelected(DeerData buck, BuckUI ui)
    {
        if (selected != default)
        {
            selected.Item2.BuckName.fontStyle = FontStyles.Underline;
        }

        selected = (buck, ui);
        CheckButtonDisabled();
        selected.Item2.BuckName.fontStyle = FontStyles.Normal;
        _emptyPreview.SetActive(false);

        // Copy of InitializeWidget
        var entry = _preview.GetComponent<BuckUI>();
        entry.gameObject.SetActive(true);
        if (entry.BuckName != null) entry.BuckName.text = buck.Name;
        entry.BuckName.ForceMeshUpdate();
        if (entry.BuckName.preferredWidth > entry.BuckName.rectTransform.rect.width) entry.BuckName.characterHorizontalScale = 0.7f;
        if (entry.BuckAppearance != null) entry.BuckAppearance.texture = buck.Controller.Rt;
        entry.Data = buck;
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

    public void BetUp(bool big)
    {
        if(MoneyManager.Instance.CanAfford(bet + _betIncrement * (big ? 5 : 1)))
        {
            bet += _betIncrement * (big ? 5 : 1);
            _betText.text = FormatDoe(bet);
        }
    }

    public void BetDown(bool big)
    {
        if (bet - _betIncrement * (big ? 5 : 1) < 0) return;
        bet -= _betIncrement * (big ? 5 : 1);
        _betText.text = FormatDoe(bet);
    }

    public void Back()
    {
        _screen.SetActive(false);

        _selectionScreen.InitializeSelection();
    }
}
