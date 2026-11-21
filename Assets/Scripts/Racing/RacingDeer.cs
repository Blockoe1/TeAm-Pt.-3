using System;
using System.Collections;
using UnityEngine;

public class RacingDeer : MonoBehaviour
{
    const float SPEED_MULT = 4;
    const float ACCEL_MULT = 0.025f;

    const float TRIP_SPEED = 2f;

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

    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private DeerController deerController;

    //Change these to use modified data instead of direct data
    private float DeerSpeed => ((30f + deerController.Data.Speed) / 50f) * SPEED_MULT;
    private float DeerAccel => ((30f + deerController.Data.Acceleration) / 50f) * ACCEL_MULT;
    private int DeerBalance => deerController.Data.Balance + 75;
    private int DeerStopChance => deerController.Data.Discipline + 58;
    private int DeerReverseChance => deerController.Data.Discipline + 79;
    private float DeerStamina => (30 - deerController.Data.Stamina) * 0.05f;

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
        StartCoroutine(TripCoroutine());
        StartCoroutine(DisciplineCoroutine());
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
                    Tripped();
                    break;
                case STATES.REVERSED:
                    Reversed();
                    break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void Running()
    {
        rigidbody.linearVelocity = Vector2.MoveTowards(rigidbody.linearVelocity, Vector2.right * DeerSpeed, DeerAccel);
    }

    private void Tripped()
    {
        if (rigidbody.linearVelocity.magnitude > 0)
        {
            rigidbody.linearVelocity = Vector2.MoveTowards(rigidbody.linearVelocity, Vector2.zero, TRIP_SPEED);
            return;
        }
        rigidbody.linearVelocity = Vector2.zero;

        currentState = STATES.NONE;
        Debug.Log("I Twibbed");
    }

    private void Reversed()
    {
        rigidbody.linearVelocity = Vector2.MoveTowards(rigidbody.linearVelocity, Vector2.left * DeerSpeed, DeerAccel);
        Debug.Log("Backwarding It");
    }

    private IEnumerator DisciplineCoroutine()
    {
        bool isStopped = false;
        while (!isStopped)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(5, 10f));
            while (currentState == STATES.NONE || currentState == STATES.REVERSED)
            {
                yield return null;
            }
            isStopped = UnityEngine.Random.Range(0, 101) > DeerStopChance;
        }

        currentState = STATES.STOPPED;
        StartCoroutine(UnStopCoroutine());
    }

    private IEnumerator UnStopCoroutine()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(3, 6f));
        if (UnityEngine.Random.Range(0, 101) > DeerReverseChance)
        {
            currentState = STATES.REVERSED;
            StartCoroutine(UnReverseCoroutine());
            yield break;
        }

        currentState = STATES.RUNNING;
        StartCoroutine(DisciplineCoroutine());
    }

    private IEnumerator UnReverseCoroutine()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 5f));
        currentState = STATES.RUNNING;
        StartCoroutine(TripCoroutine());
    }

    private IEnumerator TripCoroutine()
    {
        bool isTripped = false;
        while (!isTripped)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(7, 14f));
            while(currentState == STATES.NONE || currentState == STATES.REVERSED)
            {
                yield return null;
            }
            isTripped = UnityEngine.Random.Range(0, 101) > DeerBalance;
        }

        StartCoroutine(UnTripCoroutine());
        currentState = STATES.TRIPPED;
    }

    private IEnumerator UnTripCoroutine()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(5, 10f));
        currentState = STATES.RUNNING;
        StartCoroutine(TripCoroutine());
    }
    private void OnDestroy()
    {
        RaceManager.Broadcast_StartRace -= StartRace;
    }
}
