using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBehaviour : MonoBehaviour
{

    public ControllerData data;
   

    public bool debug;


    void Start()
    {
        data.myRigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (debug == false)
        {
            if (data.state != ControllerData.PlayerStates.Dead)
            {
                data.moveInput = new Vector3(Input.GetAxisRaw("Horizontal"+data.playerID), 0f, Input.GetAxisRaw("Vertical" + data.playerID));
                data.moveVelocity = data.moveInput * data.moveSpeed;

                Vector3 aimInput = Vector3.right * Input.GetAxisRaw("Right_Horizontal" + data.playerID) + Vector3.forward * -Input.GetAxisRaw("Right_Vertical" + data.playerID);

                if (aimInput.sqrMagnitude > 0.0f)
                {
                    transform.rotation = Quaternion.LookRotation(aimInput, Vector3.up);
                }

                if(Input.GetAxisRaw("Horizontal" + data.playerID)!=0 || Input.GetAxisRaw("Vertical" + data.playerID) != 0)
                {
                    data.isMoving = true;
                }
                else
                {
                    data.isMoving = false;
                }
            }

            else if (Input.GetButtonDown("A_Button"))
            {
                //game finie

                data.state = ControllerData.PlayerStates.Alive;
            }
        }
        else
        {
            if (Input.GetKey(data.Z))
            {
                transform.position += new Vector3(0, 0, data.moveSpeed);
            }
            if (Input.GetKey(data.S))
            {
                transform.position += new Vector3(0, 0, -data.moveSpeed);
            }
            if (Input.GetKey(data.Q))
            {
                transform.position += new Vector3(-data.moveSpeed, 0, 0);
            }
            if (Input.GetKey(data.D))
            {
                transform.position += new Vector3(data.moveSpeed, 0, 0);
            }
        }
    }

    void FixedUpdate()
    {
        if (data.state != ControllerData.PlayerStates.Dead) { 
            data.myRigidbody.velocity = data.moveVelocity;
        }
    }
}
