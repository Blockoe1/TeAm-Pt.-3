using System;
using System.Collections;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private int countdownSeconds;
    [SerializeField] private GameObject deerPrefab;

    [SerializeField] private GameObject[] deerStartPositions;

    public static DeerData[] DeerInRace => null;

    public static Action Broadcast_StartRace;
    private void Start()
    {
        //Spawn Deer w/ Stats
        for(int i = 0; i < deerStartPositions.Length; i++)
        {
            Instantiate(deerPrefab, deerStartPositions[i].transform.position, Quaternion.identity, 
                deerStartPositions[i].transform.parent);
        }
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
}
