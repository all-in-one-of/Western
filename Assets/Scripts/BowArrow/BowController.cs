﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowController : MonoBehaviour
{
    public BowData data;

    public float numberOfBullets;
    float timeBetweenShots;
    PlayerGameplayValues playerStats;

    ControllerBehaviour controllerBehaviour;

    bool noArrowShotSoundPlaying;
    float timer;

    private void Awake()
    {
        
    }

    public void Start()
    {
        playerStats = GetComponent<PlayerGameplayValues>();

        timeBetweenShots = data.timeBetweenShots;
        controllerBehaviour = GetComponent<ControllerBehaviour>();

        StartCoroutine(GiveArrows());

    }

    private IEnumerator GiveArrows()
    {
        yield return new WaitForSeconds(1);
        numberOfBullets = playerStats.numberOfArrowStartingGame;
    }


    public void Update()
    {
        timeBetweenShots -= Time.deltaTime;

        UpdateATHArrow();

        if (noArrowShotSoundPlaying == true && timer<=0)
        {
            timer = 1;
        }

        if (timer > 0 && noArrowShotSoundPlaying == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && noArrowShotSoundPlaying == true)
        {
            noArrowShotSoundPlaying = false;
        }
        //Input
        if (controllerBehaviour.data.state != ControllerData.PlayerStates.Dead)
        {
            if (Input.GetAxisRaw("Right_Trigger" + controllerBehaviour.data.playerID) == 1)
            {
                if (numberOfBullets > 0)
                {
                    Debug.Log("enough bullet");
                    data.isFiring = true;
                }
                else
                {
                    if (noArrowShotSoundPlaying == false)
                    {
                        SoundManager.instance.PlayUniqueSound(SoundManager.instance.no_arrow_shoot);
                        noArrowShotSoundPlaying = true;
                    }

                    Debug.Log("not enough bullet");
                    data.isFiring = false;
                }
            }

            else
            {
               // Debug.Log("trigger not pulled");
                data.isFiring = false;
            }
        }

        //Gun
        if (data.isFiring)
        {
            if (timeBetweenShots <= 0f)
            {
                timeBetweenShots = data.timeBetweenShots;

                SoundManager.instance.PlayArrowThrow();

            //Instantiate bullet
            GameObject newBullet = Instantiate(data.bullet, data.firePoint.position, Quaternion.Euler(data.firePoint.eulerAngles.x, data.firePoint.eulerAngles.y, data.firePoint.eulerAngles.z));
            newBullet.GetComponent<ArrowValuesGeneralNEW>().speed = data.bulletSpeed;
            Vector3 vec = data.firePoint.transform.forward * newBullet.GetComponent<ArrowValuesGeneralNEW>().speed;

            newBullet.GetComponent<Rigidbody>().AddForce(vec);
            Debug.Log(vec);

            numberOfBullets--;
            }
        }
    }

    public void UpdateATHArrow()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            if (GetComponent<ControllerBehaviour>().data.playerID == "_1")
            {
                Menu.instance.lotFlechePlayer1.transform.GetChild(i).GetComponent<Image>().sprite = Menu.instance.arrowPleine;
            }

            if (GetComponent<ControllerBehaviour>().data.playerID == "_2")
            {
                Menu.instance.lotFlechePlayer2.transform.GetChild(i).GetComponent<Image>().sprite = Menu.instance.arrowPleine;
            }
        }

        if (playerStats != null)
        {
            for (int i = (int)numberOfBullets; i < playerStats.magazineSize; i++)
            {
                if (GetComponent<ControllerBehaviour>().data.playerID == "_1")
                {
                    Menu.instance.lotFlechePlayer1.transform.GetChild(i).GetComponent<Image>().sprite = Menu.instance.arrowEmpty;
                }

                if (GetComponent<ControllerBehaviour>().data.playerID == "_2")
                {
                    Menu.instance.lotFlechePlayer2.transform.GetChild(i).GetComponent<Image>().sprite = Menu.instance.arrowEmpty;
                }
            }
        }
    }
}

