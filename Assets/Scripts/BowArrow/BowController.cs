using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        playerStats = GetComponent<PlayerGameplayValues>();

        timeBetweenShots = data.timeBetweenShots;
        controllerBehaviour = GetComponent<ControllerBehaviour>();
    }

    public void Start()
    {
        numberOfBullets = playerStats.numberOfArrowStartingGame;

    }
    public void Update()
    {
        timeBetweenShots -= Time.deltaTime;

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
            GameObject newBullet = Instantiate(data.bullet, data.firePoint.position, Quaternion.Euler(data.firePoint.eulerAngles.x, data.firePoint.eulerAngles.y, data.firePoint.eulerAngles.z), data.bulletsGroup.transform);
                newBullet.GetComponent<ArrowValuesGeneralNEW>().speed = data.bulletSpeed;
                Vector3 vec = transform.forward * newBullet.GetComponent<ArrowValuesGeneralNEW>().speed;

                newBullet.GetComponent<Rigidbody>().AddForce(vec);
                Debug.Log(vec);

                numberOfBullets--;
            }
        }
    }
}

