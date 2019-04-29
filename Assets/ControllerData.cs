using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerData : MonoBehaviour
{
    public KeyCode Z;
    public KeyCode S;
    public KeyCode Q;
    public KeyCode D;

    

    public float moveSpeed;
    [HideInInspector]
    public Rigidbody myRigidbody;

    [HideInInspector]
    public Vector3 moveInput;
    [HideInInspector]
    public Vector3 moveVelocity;

    //public GunController theGun;

    public enum PlayerStates { Alive, Dead };
    public PlayerStates state;
}
