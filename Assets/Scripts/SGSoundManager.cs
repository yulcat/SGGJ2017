using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGSoundManager : SGSingleton<SGSoundManager>{

    public AudioClip buttonSound;

    public AudioClip MainBGM;
    public AudioClip Failure;
    public AudioClip Success;
    public AudioClip Spike;
    public AudioClip Laser;
    public AudioClip Press;
    public AudioClip Playerhit;

    private static SGSoundManager SGDontInstance;

    protected override void Awake()
    {
        DontDestroyOnLoad(this);

        if (SGDontInstance == null)
        {
            SGDontInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        base.Awake();
    }

    public void PlayButtonSound()
    {
        GetComponent<AudioSource>().Stop();
        if(buttonSound != null)
            GetComponent<AudioSource>().PlayOneShot(buttonSound);
    }
    public void PlaySounds(int idx)
    {
        GetComponent<AudioSource>().Stop();
        AudioClip clip = null;
        switch (idx)
        {
            case 1:
                clip = Playerhit;
                break;
            case 2:
                clip = Failure;
                break;
            case 3:
                clip = Success;
                break;
            case 4:
                clip = Spike;
                break;
            case 5:
                clip = Laser;
                break;
            case 6:
                clip = Press;
                break;


        }

        if(clip != null)
            GetComponent<AudioSource>().PlayOneShot(clip);
    }


   }