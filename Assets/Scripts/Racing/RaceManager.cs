using NaughtyAttributes;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private int countdownSeconds;
    [SerializeField] private GameObject deerPrefab;

    [SerializeField] private GameObject[] deerStartPositions;
    [SerializeField, ReadOnly] private GameObject[] deerInRaceGO = new GameObject[5];

    [SerializeField] private CinemachineCamera camFollow;

    public static DeerData[] DeerInRace = null;
    private float[] distancesFromEnd;

    [SerializeField] private bool debugMode = false;
    [SerializeField, ShowIf("debugMode")] private DeerData[] debugDeerData;
    public static int DeerBetOn = 0;
    public static DeerData DeerBetOnDeer;

    public static Action Broadcast_StartRace;

    public static RaceManager Instance = null;
    public static DeerData WinningDeer = null;

    public GameObject[] GameObjectsInRace => deerInRaceGO;
    private void Start()
    {
        Instance = this;
        if (debugMode)
        {
            DeerInRace = debugDeerData;
        }


        //Spawn Deer w/ Stats
        distancesFromEnd = new float[DeerInRace.Length];
        for(int i = 0; i < deerStartPositions.Length; i++)
        {
            deerInRaceGO[i] = Instantiate(deerPrefab, deerStartPositions[i].transform.position, Quaternion.identity, 
                deerStartPositions[i].transform.parent);

            if(DeerInRace != null && DeerInRace[i] != null)
            {
                deerInRaceGO[i].GetComponent<DeerController>().Data = DeerInRace[i];
                deerInRaceGO[i].GetComponent<RacingDeer>().Initialize(i, distancesFromEnd);

            }

            deerInRaceGO[i].GetComponent<DeerController>().Data = DeerInRace[i];
            deerInRaceGO[i].GetComponent<DeerController>().GeneCon.VisualizeGenotypes(deerInRaceGO[i].GetComponent<DeerController>().Data);
        }

        camFollow.Follow = deerInRaceGO[DeerBetOn].transform;
        DeerBetOnDeer = deerInRaceGO[DeerBetOn].GetComponent<DeerController>().Data;
        //Race Countdown
        StartCoroutine(StartRace());
    }

    private IEnumerator StartRace()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.AudioID.SFX1);
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < countdownSeconds; i++)
        {
            //Insert graphics or sound here?
            yield return new WaitForSeconds(1);
        }
        AudioManager.Instance.PlayOneShot(AudioManager.AudioID.SFX2);
        AudioManager.Instance.PlayLoopedEvent(AudioManager.AudioID.RACEMUSIC);



        for (int i = 0; i < deerInRaceGO.Length; ++i)
        {
            TraitManager.TraitEvent(new StartRaceTrigger(i == DeerBetOn), deerInRaceGO[i].GetComponent<DeerController>());
        }
        Broadcast_StartRace?.Invoke();
    }

    public void DeerCrossedLine(DeerController deer)
    {
        //Deer Has Crossed
        WinningDeer = deer.Data;
        for (int i = 0; i < deerInRaceGO.Length; ++i)
        {
            TraitManager.TraitEvent(new EndRaceTrigger(deer), deerInRaceGO[i].GetComponent<DeerController>());

            DeerController dc = deerInRaceGO[i].GetComponent<DeerController>();
            dc.Data.LogRace(dc == deer);
        }
        SceneManager.LoadScene("PostRaceScene");
    }

    private void OnDestroy()
    {
        DeerInRace = null;
        Instance = null;
    }
}
