using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform Player1;
    public Transform Player2;

    public Camera camComponent;

    public int minCamSize;
    public int maxCamSize;

    Vector3 newcamPosition;
    float timer;

    
    public void Update()
    {
        float rangeBewteenPlayers = Vector3.Distance(Player1.position, Player2.position);
        
        Vector3 positionToGo = (Player1.position + Player2.position) / 2;
        Vector3 positionToTake = positionToGo += (transform.forward * -25);

        transform.position = Vector3.Lerp(transform.position, positionToTake, Time.deltaTime*10);

        float camSize = rangeBewteenPlayers / 1.2f;

        float fielOfView = Mathf.Clamp(camSize, minCamSize, maxCamSize);
        camComponent.orthographicSize = Mathf.Lerp(camComponent.fieldOfView, fielOfView, Time.deltaTime * 100);
    }

}
