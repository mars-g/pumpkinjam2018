using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static bool IsActive() { return instance != null; }
    private static AudioManager curInstance;
    private static AudioManager instance { get { return curInstance; } }

    public AudioSource playersounds;

    public AudioClip swingmiss, swinghit, playerhurt, playerjump, pickuporb;

    // Use this for initialization
    void Start()
    {
        if (curInstance == null) { curInstance = this; }
    }

    void StopPlayerSounds()
    {
        if (playersounds.isPlaying) playersounds.Stop();
    }

    void PlayPlayerSounds(AudioClip newclip)
    {
        playersounds.clip = newclip;
        playersounds.Play();
    }

    public static void SwingHit()
    {
        instance.StopPlayerSounds();
        instance.PlayPlayerSounds(instance.swinghit);
        
    }
    public static void SwingMiss()
    {
        instance.StopPlayerSounds();
        instance.PlayPlayerSounds(instance.swingmiss);

    }
    public static void PlayerJump()
    {
        instance.StopPlayerSounds();
        instance.PlayPlayerSounds(instance.playerjump);
    }
    public static void PlayerHurt()
    {
        instance.StopPlayerSounds();
        instance.PlayPlayerSounds(instance.playerhurt);
    }
    public static void PickUpOrb()
    {
        instance.StopPlayerSounds();
        instance.PlayPlayerSounds(instance.pickuporb);
    }


}
