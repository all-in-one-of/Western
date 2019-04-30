using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerBehaviour : MonoBehaviour
{

    public ControllerData data;

    LineRenderer lineRenderer;
    BowController bowController;

    float timeBewteenDash;
    public float currentEndurance;
    bool readyToDash;
    public float DashAmount;

    void Start()
    {
        data.myRigidbody = GetComponent<Rigidbody>();
        bowController = GetComponent<BowController>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.SetPosition(0, bowController.data.firePoint.position);

        lineRenderer.SetPosition(1, transform.forward * 10000);

        currentEndurance = data.enduranceMax;
    }


    void Update()
    {

        data.enduranceJauge.fillAmount = currentEndurance / data.enduranceMax;

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

            data.moveVelocity = data.moveInput * data.moveSpeed;

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

            if (Input.GetAxisRaw("Left_Trigger" + data.playerID) != 0 && currentEndurance >= 0.5f && readyToDash == true && data.moveInput != Vector3.zero)
            {
            
                //dasH
                data.myRigidbody.AddForce(data.moveInput * DashAmount);
                currentEndurance -= 0.5f;
                timeBewteenDash = data.timeBetweenEachDash;
                readyToDash = false;

            }
            else if (data.moveInput == Vector3.zero)
            {
                //no input dash
            }
        }

        else if (Input.GetButtonDown("A_Button"))
        {
            //game finie

            data.state = ControllerData.PlayerStates.Alive;
        }
    }

    public void FillingUpendurance()
    {
        if (currentEndurance < data.enduranceMax )
        {
            currentEndurance += Time.deltaTime * 0.05f;
        }
    }

    void FixedUpdate()
    {
        if (data.state != ControllerData.PlayerStates.Dead )
        { 
            data.myRigidbody.velocity = data.moveVelocity;
        }
    }
}
