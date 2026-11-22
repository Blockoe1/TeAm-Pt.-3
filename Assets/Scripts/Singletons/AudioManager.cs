using UnityEngine;
using FMODUnity;
using System.Collections.Generic;
using FMOD.Studio;
using MarUtility.ExecutionManagement;
using UnityEngine.SceneManagement;

public class AudioManager : Manager
{
    public enum AudioID
    {
        NONE = -1,
        IDLEMUSIC = 0,
        RACEMUSIC = 1,
        SFX1 = 2,
        SFX2 = 3
    }
    private AudioID currentMusicID = AudioID.NONE;

    [SerializeField] private EventReference[] audioLookup;

    public static AudioManager Instance = null;

    private EventInstance bgMusic;

    public override void Initialize()
    {
        if (Instance == null)
        {
            base.Initialize();
            Instance = this;

            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            PlayLoopedEvent(AudioID.IDLEMUSIC);
            Debug.Log(audioLookup.Length);
            return;
        }

        Debug.LogError("How did we get here?\nWe have an extra copy of AudioManager, deleting the copy...");
        Destroy(gameObject);
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if(arg1.name == "RaceScene")
        {
            PlayLoopedEvent(AudioID.NONE);
        }
        else
        {
            PlayLoopedEvent(AudioID.IDLEMUSIC);
        }
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    /// <summary>
    /// Plays a 1shot FMOD sound. Don't make this a piece of music we can't stop it
    /// </summary>
    /// <param name="audioID">The audio to be played</param>
    public void PlayOneShot(AudioID audioID)
    {
        if (audioID == AudioID.NONE)
        {
            return;
        }

        RuntimeManager.PlayOneShot(audioLookup[(int)audioID]);
    }

    /// <summary>
    /// Plays a looped sound for the background music. Please do not input a sound effect I will find you and make very upset Lucas noises if you do :(
    /// </summary>
    /// <param name="audioID">The passed Audio. Please don't make this a sound effect</param>
    public void PlayLoopedEvent(AudioID audioID)
    {
        if(currentMusicID != AudioID.NONE && currentMusicID == audioID)
        {
            return;
        }

        bgMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);


        if (audioID == AudioID.NONE)
        {
            return;
        }

        bgMusic = RuntimeManager.CreateInstance(audioLookup[(int)audioID]);

        bgMusic.start();
        currentMusicID = audioID;
    }
}
