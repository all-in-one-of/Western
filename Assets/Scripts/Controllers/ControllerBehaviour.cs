using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerBehaviour : MonoBehaviour
{
    public ControllerData data;

    LineRenderer lineRenderer;
    BowController bowController;

    PlayerGameplayValues playerStats;

    float timeBewteenDash;
    bool readyToDash;

    bool coroutineLaunched;
    public float DashStrengh;

    public float DashCost;

    bool noStaminaSoundPlaying;
    float timer;

    void Start()
    {
        playerStats = GetComponent<PlayerGameplayValues>();

        data.myRigidbody = GetComponent<Rigidbody>();
        bowController = GetComponent<BowController>();
        lineRenderer = GetComponent<LineRenderer>();

        //setup line renderer
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.SetPosition(0, bowController.data.firePoint.position);

        lineRenderer.SetPosition(1, transform.forward * 10000);

    }


    void Update()
    {
        data.enduranceJauge.fillAmount = playerStats.stamina / playerStats.maxStamina;

        if (noStaminaSoundPlaying == true && timer <= 0)
        {
            timer = 1;
        }

        if (timer > 0 && noStaminaSoundPlaying == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && noStaminaSoundPlaying == true)
        {
            noStaminaSoundPlaying = false;
        }


        ///data.enduranceJauge.fillAmount = currentEndurance / data.enduranceMax;

        FillingUpendurance();

        if (timeBewteenDash<= 0)
        {
            readyToDash = true;
        }
        else if (timeBewteenDash>0 && readyToDash==false)
        {
            timeBewteenDash -= Time.deltaTime;
        }

        if (data.state != ControllerData.PlayerStates.Dead)
        {
            
                data.moveInput = new Vector3(Input.GetAxisRaw("Horizontal" + data.playerID), 0f, Input.GetAxisRaw("Vertical" + data.playerID));

            data.moveVelocity = data.moveInput * playerStats.speed;

            Vector3 aimInput = Vector3.right * Input.GetAxisRaw("Right_Horizontal" + data.playerID) + Vector3.forward * -Input.GetAxisRaw("Right_Vertical" + data.playerID);


            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;
            lineRenderer.SetPosition(0, bowController.data.firePoint.position);

            if (aimInput.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(aimInput, Vector3.up);

                   
                lineRenderer.SetPosition(1, aimInput * 10000);
            }

            if(Input.GetAxisRaw("Horizontal" + data.playerID)!=0 || Input.GetAxisRaw("Vertical" + data.playerID) != 0 )
            {
                data.isMoving = true;
            }
            else
            {
                data.isMoving = false;
            }

            if (Input.GetAxisRaw("Left_Trigger" + data.playerID) != 0 && playerStats.stamina > 0 && readyToDash == true && data.moveInput != Vector3.zero)
            {

                //dasH
                SoundManager.instance.PlayDash();

                data.myRigidbody.AddForce(data.moveInput * DashStrengh);
                if (playerStats.stamina - DashCost >= 0)
                {
                    playerStats.stamina -= DashCost;
                }
                else
                {
                    playerStats.stamina = 0;
                }
                timeBewteenDash = data.timeBetweenEachDash;
                readyToDash = false;

            }
            else if (data.moveInput == Vector3.zero)
            {
                //no input dash
            }
            else if (playerStats.stamina <= 0)
            {
                if (noStaminaSoundPlaying == false)
                {
                    SoundManager.instance.PlayNoEnergy();
                    noStaminaSoundPlaying = true;
                }
            }
        }

        else if (Input.GetButtonDown("A_Button" + data.playerID))
        {
            //game finie
            data.state = ControllerData.PlayerStates.Alive;
        }
    }

    public void FillingUpendurance()
    {
        if (playerStats.stamina < playerStats.maxStamina && playerStats.stamina!= 0)
        {
            playerStats.stamina += Time.deltaTime * playerStats.staminaRegen;
        }
        else if (playerStats.stamina == 0)
        {
            if (coroutineLaunched == false)
            {
                StartCoroutine(waitForSeconds());
            }
            
        }
    }

    public IEnumerator waitForSeconds()
    {
        coroutineLaunched = true;
        yield return new WaitForSeconds(2);

        playerStats.stamina = 1f;
        coroutineLaunched = false;

    }

    void FixedUpdate()
    {
        if (data.state != ControllerData.PlayerStates.Dead )
        { 
            data.myRigidbody.velocity = data.moveVelocity;
        }
    }
}
