using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public BowData data;

    int numberOfBullets;
    float timeBetweenShots;

    ControllerBehaviour controllerBehaviour;

    private void Awake()
    {
        numberOfBullets = data.magazineSize;
        timeBetweenShots = data.timeBetweenShots;
    }
    public void Update()
    {
        timeBetweenShots -= Time.deltaTime;
       
        //Input
        if (controllerBehaviour.data.state != ControllerData.PlayerStates.Dead)
        {
            if (Input.GetAxisRaw("Right_Trigger") == 1)
            {
                if (numberOfBullets > 0)
                {
                    data.isFiring = true;
                }
                else
                {
                    data.isFiring = false;
                }
            }

            else
            {
                data.isFiring = false;
            }
        }

        //Gun
        if (data.isFiring)
        {
            if (timeBetweenShots <= 0f)
            {
                timeBetweenShots = data.timeBetweenShots;

                //Instantiate bullet
                ArrowMovingBehaviour newBullet = Instantiate(data.bullet, data.firePoint.position, Quaternion.identity, transform);
                newBullet.speed = data.bulletSpeed;

                numberOfBullets--;
            }
        }
    }
}

