using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerBehaviour : MonoBehaviour
{
    public ControllerData data;
    public PlayerBehaviour playerBehaviour;

    public ParticleSystem dashParticle;
    LineRenderer lineRenderer;
    BowController bowController;

    PlayerGameplayValues playerStats;

    public GameObject objetquitourne;
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

        lineRenderer.SetPosition(1, transform.forward * data.lineRange);

        if (data.playerID == "_0")
        {
            GameManagerValues.instance.Player1 = this;
        }
        else
        {
            GameManagerValues.instance.Player2 = this;

        }

    }


    void Update()
    {


        if (playerStats != null)
        {
            data.enduranceJauge.fillAmount = playerStats.stamina / playerStats.maxStamina;
        }
        else
        {
            playerStats = GetComponent<PlayerGameplayValues>();

        }

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

            if (playerStats != null)
            {
                data.moveVelocity = data.moveInput * playerStats.speed;
            }

            Vector3 aimInput = Vector3.right * Input.GetAxisRaw("Right_Horizontal" + data.playerID) + Vector3.forward * -Input.GetAxisRaw("Right_Vertical" + data.playerID);

            if (aimInput.z <= 0) {
                playerBehaviour.animator.SetBool("GoingUp", false);
                playerBehaviour.bowSpriteRenderer.sortingOrder = 50;
            }
            else
            {
                playerBehaviour.animator.SetBool("GoingUp", true);
                playerBehaviour.bowSpriteRenderer.sortingOrder = -50;
            }

            if (aimInput.x >= 0)
            {
                playerBehaviour.playerSpriteRenderer.flipX = true;
            }
            else
            {
                playerBehaviour.playerSpriteRenderer.flipX = false;
            }



            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;

            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, bowController.data.firePoint.position);
            

            if (aimInput.sqrMagnitude > 0.0f)
            {
                playerBehaviour.parentBow.transform.eulerAngles = new Vector3(playerBehaviour.parentBow.transform.eulerAngles.x, playerBehaviour.parentBow.transform.eulerAngles.y, Quaternion.LookRotation(aimInput, Vector3.up).eulerAngles.y+90) ;
                objetquitourne.transform.rotation = Quaternion.LookRotation(aimInput, Vector3.up);





                //lineRenderer.SetPosition(1, aimInput * data.lineRange);
                Bounce(aimInput.normalized, data.lineRange);
            }

            if(Input.GetAxisRaw("Horizontal" + data.playerID)!=0 || Input.GetAxisRaw("Vertical" + data.playerID) != 0 )
            {
                data.isMoving = true;
            }
            else
            {
                data.isMoving = false;
            }

            playerBehaviour.animator.SetBool("Moving", data.isMoving);

            if (Input.GetAxisRaw("Left_Trigger" + data.playerID) != 0 && playerStats.stamina > 0 && readyToDash == true && data.moveInput != Vector3.zero)
            {

                //dasH
                SoundManager.instance.PlayDash();
                dashParticle.Play();

                data.myRigidbody.AddForce(data.moveInput * DashStrengh);

                dashParticle.gameObject.transform.LookAt(-(data.moveInput * DashStrengh));

                dashParticle.gameObject.transform.localPosition = Vector3.zero;


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
                StartCoroutine(waitForSecondsDash(0.2f));

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

    IEnumerator waitForSecondsDash(float timer)
    {
        yield return new WaitForSeconds(timer);
        dashParticle.Stop();

    }
    private void Bounce(Vector3 direction,float remainingRange)
    {
        int layerMask = 1 << 8 | 1 << 12;
        layerMask = ~layerMask;
        RaycastHit raycastHit;
        lineRenderer.positionCount++;
        if (remainingRange > 0)
        {
            if (Physics.Raycast(bowController.data.firePoint.position, direction, out raycastHit, remainingRange, layerMask))
            {
                if (raycastHit.collider.CompareTag("SurfaceRebond"))
                {
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, raycastHit.point);
                    Vector3 bounceDirection = Vector3.Reflect(direction, raycastHit.normal);
                    bounceDirection = new Vector3(bounceDirection.x, 0, bounceDirection.y);
                    remainingRange -= raycastHit.distance;

                    Bounce(bounceDirection.normalized, remainingRange);
                }
                else
                {
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, lineRenderer.GetPosition(lineRenderer.positionCount - 2) + direction * remainingRange);
                }

            }
            else
            {
                lineRenderer.SetPosition(lineRenderer.positionCount - 1,lineRenderer.GetPosition(lineRenderer.positionCount-2)+ direction * remainingRange);
            }
        }
        
    }

    public void FillingUpendurance()
    {
        if (playerStats != null)
        {
            if (playerStats.stamina < playerStats.maxStamina && playerStats.stamina != 0)
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
