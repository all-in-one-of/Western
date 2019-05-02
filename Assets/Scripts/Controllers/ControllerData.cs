using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerData : MonoBehaviour
{
    public string playerID;
    public CameraBehaviour mainCamera;

    [HideInInspector]
    public Rigidbody myRigidbody;

    [HideInInspector]
    public Vector3 moveInput;
    [HideInInspector]
    public Vector3 moveVelocity;

    //public GunController theGun;

    public enum PlayerStates { Alive, Dead };
    public PlayerStates state;

    public bool isMoving;

    public Image enduranceJauge;
    public float timeBetweenEachDash;

}
