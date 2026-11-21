using System;
using System.Collections;
using UnityEngine;

public class RacingDeer : MonoBehaviour
{
    //I apologize in advance
    private enum STATES
    {
        NONE,
        RUNNING,
        STOPPED,
        TRIPPED,
        REVERSED
    }

    private STATES currentState = STATES.NONE;

    private Rigidbody2D rigidbody;
    private float currentSpeed = 0f;
    /// <summary>
    /// Will start when the deer gets started
    /// </summary>
    private void Start()
    {
        RaceManager.Broadcast_StartRace += StartRace;
    }

    private void StartRace()
    {
        currentState = STATES.RUNNING;
        StartCoroutine(Movement());
    }

    /// <summary>
    /// Controls all other form of movement
    /// </summary>
    /// <returns></returns>
    private IEnumerator Movement()
    {
        while (true)
        {
            switch (currentState)
            {
                case STATES.RUNNING:
                    Running();
                    break;
                case STATES.TRIPPED:
                    Tripped();
                    break;
                case STATES.STOPPED:
                    Stopped();
                    break;
                case STATES.REVERSED:
                    Reversed();
                    break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void Running()
    {

    }

    private void Tripped()
    {

    }

    private void Stopped()
    {

    }

    private void Reversed()
    {

    }

    private void OnDestroy()
    {
        RaceManager.Broadcast_StartRace -= StartRace;
    }
}
