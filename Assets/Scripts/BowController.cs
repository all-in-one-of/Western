using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public BowData data;

    public int numberOfBullets;
    float timeBetweenShots;

    ControllerBehaviour controllerBehaviour;

    private void Awake()
    {
        numberOfBullets = data.magazineSize-1;
        timeBetweenShots = data.timeBetweenShots;
        controllerBehaviour = GetComponent<ControllerBehaviour>();
    }
    public void Update()
    {
        timeBetweenShots -= Time.deltaTime;

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

                //Instantiate bullet
                GameObject newBullet = Instantiate(data.bullet, data.firePoint.position, Quaternion.Euler(data.firePoint.eulerAngles.x, data.firePoint.eulerAngles.y, data.firePoint.eulerAngles.z), data.bulletsGroup.transform);
                newBullet.GetComponent<ArrowMovingBehaviour>().speed = data.bulletSpeed;

                newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * newBullet.GetComponent<ArrowMovingBehaviour>().speed);


                numberOfBullets--;
            }
        }
    }
}

