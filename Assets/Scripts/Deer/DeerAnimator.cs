using MarUtility.ExecutionManagement;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeerAnimator : Manager
{
    //PARAMETER ID
    [SerializeField, BoxGroup("Parameter ID")]
    private string _fDeerSpeedID;
    [SerializeField, BoxGroup("Parameter ID")]
    private string _tTripID;
    [SerializeField, BoxGroup("Parameter ID")]
    private string _tDieID;

    private Animator anim;

    public override void Initialize()
    {
        anim = GetComponent<Animator>();

        base.Initialize();
    }

    public void SetSpeed(float value)
        => anim.SetFloat(_fDeerSpeedID, value);

    public void TriggerTrip()
        => anim.SetTrigger(_tTripID);

    public void TriggerDie()
        => anim.SetTrigger(_tDieID);
}
