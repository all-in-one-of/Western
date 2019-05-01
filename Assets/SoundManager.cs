using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip arrowBounce;
   
    public AudioClip arrowHit1;
    public AudioClip arrowHit2;
    public AudioClip arrowHit3;
    public AudioClip arrowHit4;

    public AudioClip arrowTake1;
    public AudioClip arrowTake2;
    public AudioClip arrowTake3;
   
    public AudioClip arrowThrow1;
    public AudioClip arrowThrow2;
    public AudioClip arrowThrow3;
    public AudioClip arrowThrow4;
    public AudioClip combatStart;
    public AudioClip combatEnd;

    public AudioClip dash1;
    public AudioClip dash2;
    public AudioClip dash3;
    public AudioClip dash4;

    public AudioClip die1;
    public AudioClip die2;
    public AudioClip die3;

    public AudioClip gameOver;

    public AudioClip gun1;
    public AudioClip gun2;
    public AudioClip gun3;
    public AudioClip gun4;
    public AudioClip gun5;
    public AudioClip gun6;

    public AudioClip newGame;
    public AudioClip no_arrow_shoot;

    public AudioClip noEndurance1;
    public AudioClip noEndurance2;
    public AudioClip noEndurance3;

    public AudioClip revive;
    public AudioClip ui;
    public AudioClip win_level;

    AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayUniqueSound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayArrowHit()
    {
        int r = Random.Range(0, 4);

        if (r == 0)
        {
            audioSource.PlayOneShot(arrowHit1);
        }
        else if (r == 1)
        {
            audioSource.PlayOneShot(arrowHit2);
        }
        else if (r == 2)
        {
            audioSource.PlayOneShot(arrowHit3);
        }
        else if (r == 3)
        {
            audioSource.PlayOneShot(arrowHit4);
        }
    }

    public void PlayArrowTake()
    {
        int r = Random.Range(0, 3);

        if (r == 0)
        {
            audioSource.PlayOneShot(arrowTake1);
        }
        else if (r == 1)
        {
            audioSource.PlayOneShot(arrowTake2);
        }
        else if (r == 2)
        {
            audioSource.PlayOneShot(arrowTake3);
        }
       
    }

    public void PlayArrowThrow()
    {
        int r = Random.Range(0, 4);

        if (r == 0)
        {
            audioSource.PlayOneShot(arrowThrow1);
        }
        else if (r == 1)
        {
            audioSource.PlayOneShot(arrowThrow2);
        }
        else if (r == 2)
        {
            audioSource.PlayOneShot(arrowThrow3);
        }
        else if (r == 3)
        {
            audioSource.PlayOneShot(arrowThrow4);
        }
    }

    public void PlayDash()
    {
        int r = Random.Range(0, 4);

        if (r == 0)
        {
            audioSource.PlayOneShot(dash1);
        }
        else if (r == 1)
        {
            audioSource.PlayOneShot(dash2);
        }
        else if (r == 2)
        {
            audioSource.PlayOneShot(dash3);
        }
        else if (r == 3)
        {
            audioSource.PlayOneShot(dash4);
        }
    }

    public void PlayDie()
    {
        int r = Random.Range(0, 3);

        if (r == 0)
        {
            audioSource.PlayOneShot(die1);
        }
        else if (r == 1)
        {
            audioSource.PlayOneShot(die2);
        }
        else if (r == 2)
        {
            audioSource.PlayOneShot(die3);
        }

    }

    public void PlayGun()
    {
        int r = Random.Range(0, 6);

        if (r == 0)
        {
            audioSource.PlayOneShot(gun1);
        }
        else if (r == 1)
        {
            audioSource.PlayOneShot(gun2);
        }
        else if (r == 2)
        {
            audioSource.PlayOneShot(gun3);
        }
        else if (r == 3)
        {
            audioSource.PlayOneShot(gun4);
        }
        else if (r == 4)
        {
            audioSource.PlayOneShot(gun5);
        }
        else if (r == 5)
        {
            audioSource.PlayOneShot(gun6);
        }
       

    }

    public void PlayNoEnergy()
    {
        int r = Random.Range(0, 3);

        if (r == 0)
        {
            audioSource.PlayOneShot(noEndurance1);
        }
        else if (r == 1)
        {
            audioSource.PlayOneShot(noEndurance2);
        }
        else if (r == 2)
        {
            audioSource.PlayOneShot(noEndurance3);
        }
    }
}
