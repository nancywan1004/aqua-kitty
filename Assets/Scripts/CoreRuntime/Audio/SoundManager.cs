using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip alertSound, bubbleHitSound, popSound, meowSound, grappleSound, background1, background2, attackBackground;
    private static AudioSource audioSrc;
    // Start is called before the first frame update

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        background2 = Resources.Load<AudioClip>("background2");
    }
    void Start()
    {
        alertSound = Resources.Load<AudioClip>("alert_sound_effect");
        bubbleHitSound = Resources.Load<AudioClip>("bubbleHit");
        popSound = Resources.Load<AudioClip>("pop");
        meowSound = Resources.Load<AudioClip>("meow");
        grappleSound = Resources.Load<AudioClip>("grapple");
        background1 = Resources.Load<AudioClip>("background1");
        //background2 = Resources.Load<AudioClip>("background2");
        attackBackground = Resources.Load<AudioClip>("attacking");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "alert":
                audioSrc.PlayOneShot(alertSound);
                break;
            case "bubbleHit":
                audioSrc.PlayOneShot(bubbleHitSound);
                break;
            case "bubblePop":
                audioSrc.PlayOneShot(popSound);
                break;
            case "meow":
                audioSrc.PlayOneShot(meowSound);
                break;
            case "grapple":
                audioSrc.PlayOneShot(grappleSound);
                break;
            case "background1":
                if (audioSrc)
                {
                    audioSrc.Stop();
                    audioSrc.loop = true;
                    audioSrc.clip = background1;
                    audioSrc.volume = 0.5f;
                    audioSrc.Play();
                }
                break;
            case "background2":
                if (audioSrc)
                {
                    audioSrc.Stop();
                    audioSrc.loop = true;
                    audioSrc.clip = background2;
                    audioSrc.volume = 0.3f;
                    audioSrc.Play();
                    Debug.Log("background2 is playing: " + audioSrc.clip);
                }
                break;
            case "attackBackground":
                if (audioSrc)
                {
                    audioSrc.Stop();
                    audioSrc.loop = true;
                    audioSrc.clip = attackBackground;
                    audioSrc.volume = 0.2f;
                    audioSrc.Play();
                }
                break;
        }
        }

    public static void StopSound ()
    {
        if (audioSrc && audioSrc.isPlaying)
        {
            audioSrc.Stop();
        }
    }

    public static void PauseSound ()
    {
        audioSrc.Pause();
    }

    public static void ResumeSound()
    {
        audioSrc.UnPause();
    }

}
