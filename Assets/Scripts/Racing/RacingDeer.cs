using System.Collections;
using UnityEngine;

public class RacingDeer : MonoBehaviour
{
    const float SPEED_MULT = 8.8f;
    const float ACCEL_MULT = 0.022f;
    const float STAMINA_MULT = 0.0034f;

    const float TRIP_SPEED = 2f;
    private const float ENDING_POSITION = 88;

    //I apologize in advance
    public enum STATES
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
    private float speedMod = 0;
    private float speedModMult = 1f;
    private float accelModMult = 1f;
    private float DeerSpeed => (((30f + deerController.GetStat(StatType.Speed)) / 50f * SPEED_MULT) - speedMod) * speedModMult;
    private float DeerAccel => (30f + deerController.GetStat(StatType.Acceleration)) / 50f * ACCEL_MULT * accelModMult;
    private int DeerBalance => deerController.GetStat(StatType.Balance) + 75;
    private float DeerStopChance => deerController.GetStat(StatType.Discipline) * 1.5f + 65;
    private int DeerReverseChance => deerController.GetStat(StatType.Discipline) + 50;
    private float DeerStamina => (30 - deerController.GetStat(StatType.Stamina)) * STAMINA_MULT;

    public STATES CurrentState { get { return currentState; } set { currentState = value; }  }

    private int placementIndex;
    private float[] placements;
    [SerializeField] private int placement;
    private bool inRace;


    /// <summary>
    /// Will start when the deer gets started
    /// </summary>
    private void Start()
    {
        EditStats();
        RaceManager.Broadcast_StartRace += StartRace;
    }

    private void FixedUpdate()
    {
        deerController.Anim.SetSpeed(rigidbody.linearVelocityX);
        Debug.Log(deerController.Data.Name + ": " + rigidbody.linearVelocityX);
    }

    private void EditStats()
    {
        name = deerController.Data.Name;
    }

    public void Initialize(int placementIndex, float[] placements)
    {
        this.placementIndex = placementIndex;
        this.placements = placements;
        inRace = true;
    }

    private void StartRace()
    {
        if (inRace)
        {
            Debug.Log(currentState);
            if (currentState == STATES.NONE)
            {
                currentState = STATES.RUNNING;
            }
            Debug.Log(currentState);
            if (currentState == STATES.TRIPPED)
            {
                TripTrigger trigger = new TripTrigger(UnityEngine.Random.Range(2, 4f));
                TraitManager.TraitEvent(trigger, deerController);
                StartCoroutine(UnTripCoroutine(trigger.TripTime));
            }
            else
            {
                StartCoroutine(TripCoroutine());
            }
            StartCoroutine(DisciplineCoroutine());
            StartCoroutine(SpeedLower());
            StartCoroutine(SpeedRandom());
            StartCoroutine(AccelRandom());
            StartCoroutine(Movement());
            inRace = false;
        }
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
            UpdatePlacement();
            if (transform.position.x > ENDING_POSITION)
            {
                RaceManager.Instance.DeerCrossedLine(deerController);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void UpdatePlacement()
    {
        float distance = ENDING_POSITION - transform.position.x;
        placements[placementIndex] = distance;
        int newPlacement = 1;
        for(int i = 0; i < placements.Length; i++)
        {
            if (placements[i] < distance)
            {
                newPlacement++;
            }
        }

        if (newPlacement != placement)
        {
            placement = newPlacement;
            TraitManager.TraitEvent(new PlacementChangeTrigger(placement), deerController);
        }
    }

    private void Running()
    {
        float maxSpeed = DeerSpeed > 1 ? DeerSpeed : 1;
        rigidbody.linearVelocity = Vector2.MoveTowards(rigidbody.linearVelocity, Vector2.right * maxSpeed, DeerAccel);
    }

    private void Tripped()
    {
        if (rigidbody.linearVelocity.magnitude > 0)
        {
            rigidbody.linearVelocity = Vector2.MoveTowards(rigidbody.linearVelocity, Vector2.zero, TRIP_SPEED);
            return;
        }
        rigidbody.linearVelocity = Vector2.zero;
        deerController.Anim.TriggerTrip();

        currentState = STATES.NONE;
    }

    private void Reversed()
    {
        rigidbody.linearVelocity = Vector2.MoveTowards(rigidbody.linearVelocity, Vector2.left * DeerSpeed, DeerAccel);
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

        DisobeyTrigger trigger = new DisobeyTrigger(currentState == STATES.REVERSED, UnityEngine.Random.Range(1, 3f));
        TraitManager.TraitEvent(trigger, deerController);
        currentState = STATES.STOPPED;
        StartCoroutine(UnStopCoroutine(trigger.DisobeyTime));
    }

    private IEnumerator UnStopCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
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
        yield return new WaitForSeconds(UnityEngine.Random.Range(1, 3f));
        currentState = STATES.RUNNING;
        StartCoroutine(TripCoroutine());
    }

    private IEnumerator TripCoroutine()
    {
        bool isTripped = false;
        while (!isTripped)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(4, 6f));
            while(currentState == STATES.NONE || currentState == STATES.REVERSED)
            {
                yield return null;
            }
            isTripped = UnityEngine.Random.Range(0, 101) > DeerBalance;
        }

        TripTrigger trigger = new TripTrigger(UnityEngine.Random.Range(2, 4f));
        TraitManager.TraitEvent(trigger, deerController);
        StartCoroutine(UnTripCoroutine(trigger.TripTime));
        currentState = STATES.TRIPPED;
    }

    public void Trip()
    {
        if (currentState != STATES.TRIPPED)
        {
            StopCoroutine(TripCoroutine());
            CurrentState = RacingDeer.STATES.TRIPPED;
            StartCoroutine(UnTripCoroutine(UnityEngine.Random.Range(2, 4f)));
        }
    }

    private IEnumerator UnTripCoroutine(float time)
    {
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        currentState = STATES.RUNNING;
        StartCoroutine(TripCoroutine());
    }

    private IEnumerator SpeedLower()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            while(currentState == STATES.NONE || currentState == STATES.REVERSED)
            {
                yield return null;
            }
            speedMod += DeerStamina * DeerSpeed;
        }
    }
    private IEnumerator SpeedRandom()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            speedModMult *= UnityEngine.Random.Range(0.95f, 1.05f);
        }
    }

    private IEnumerator AccelRandom()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            accelModMult *= UnityEngine.Random.Range(0.95f, 1.05f);
        }
    }

    private void OnDestroy()
    {
        RaceManager.Broadcast_StartRace -= StartRace;
    }
}
