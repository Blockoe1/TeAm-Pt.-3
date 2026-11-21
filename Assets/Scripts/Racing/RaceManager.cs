using System;
using System.Collections;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] private int countdownSeconds;

    public static DeerData[] DeerInRace => null;

    public static Action Broadcast_StartRace;
    private void Start()
    {
        //Spawn Deer w/ Stats
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
