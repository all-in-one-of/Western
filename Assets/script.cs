using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    public CameraBehaviour cam;

    public Transform player1;
    public Transform player2;


    // Start is called before the first frame update
    void Start()
    {
        cam.Player1 = player1;
        cam.Player2 = player2;
        cam.Activate(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
