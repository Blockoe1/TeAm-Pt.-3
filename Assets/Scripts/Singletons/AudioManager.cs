using UnityEngine;
using FMODUnity;
using System.Collections.Generic;
using FMOD.Studio;
using MarUtility.ExecutionManagement;

public class AudioManager : Manager
{
    public enum AudioID
    {
        NONE = 0,
        IDLEMUSIC = 1,
        RACEMUSIC = 2,
        SFX1 = 3,
        SFX2 = 4
    }
    [SerializeField] private EventReference[] audioLookup;

    public static AudioManager Instance = null;

    private EventInstance bgMusic;

    public override void Initialize()
    {
        if (Instance == null)
        {
            base.Initialize();
            Instance = this;


            return;
        }

        Debug.LogError("How did we get here?\nWe have an extra copy of AudioManager, deleting the copy...");
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Plays a 1shot FMOD sound. Don't make this a piece of music we can't stop it
    /// </summary>
    /// <param name="audioID">The audio to be played</param>
    public void PlayOneShot(AudioID audioID)
    {
        RuntimeManager.PlayOneShot(audioLookup[(int)audioID]);
    }

    /// <summary>
    /// Plays a looped sound for the background music. Please do not input a sound effect I will find you and make very upset Lucas noises if you do :(
    /// </summary>
    /// <param name="audioID">The passed Audio. Please don't make this a sound effect</param>
    public void PlayLoopedEvent(AudioID audioID)
    {
        bgMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        bgMusic = RuntimeManager.CreateInstance(audioLookup[(int)audioID]);

        bgMusic.start();
    }
}
