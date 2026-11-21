using NaughtyAttributes;
using NUnit.Framework;
using System;
using System.Collections;
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

    [SerializeField] private bool debugMode = false;
    [SerializeField, ShowIf("debugMode")] private DeerData[] debugDeerData;
    public static int DeerBetOn = 0;

    public static Action Broadcast_StartRace;

    public static RaceManager Instance = null;
    public static DeerData WinningDeer = null;
    private void Start()
    {
        Instance = this;
        if (debugMode)
        {
            DeerInRace = debugDeerData;
        }
        //Spawn Deer w/ Stats
        for(int i = 0; i < deerStartPositions.Length; i++)
        {
            deerInRaceGO[i] = Instantiate(deerPrefab, deerStartPositions[i].transform.position, Quaternion.identity, 
                deerStartPositions[i].transform.parent);

            if(DeerInRace != null && DeerInRace[i] != null)
            {
                deerInRaceGO[i].GetComponent<DeerController>().Data = DeerInRace[i];
            }
        }

        camFollow.Follow = deerInRaceGO[DeerBetOn].transform;
        //Race Countdown
        StartCoroutine(StartRace());
    }

    private IEnumerator StartRace()
    {
        for (int i = 0; i < countdownSeconds; i++)
        {
            //Insert graphics or sound here?
            yield return new WaitForSeconds(1);
        }

        Broadcast_StartRace?.Invoke();
    }

    public void DeerCrossedLine(DeerData deer)
    {
        //Deer Has Crossed
        WinningDeer = deer;
        SceneManager.LoadScene("PostRaceScene");
    }

    private void OnDestroy()
    {
        DeerInRace = null;
        Instance = null;
    }
}
