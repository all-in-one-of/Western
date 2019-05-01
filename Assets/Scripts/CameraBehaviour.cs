using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform Player1;
    public Transform Player2;

    public Camera camComponent;

    Vector3 newcamPosition;
    float timer;

    
    public void Update()
    {
        float rangeBewteenPlayers = Vector3.Distance(Player1.position, Player2.position);
        
        Vector3 positionToGo = (Player1.position + Player2.position) / 2;
        Vector3 positionToTake = new Vector3(positionToGo.x - 0.3f, positionToGo.y + 11.41f, positionToGo.z - 9.37f);

        transform.position = Vector3.Lerp(transform.position, positionToTake, Time.deltaTime*10);

        float camSize = rangeBewteenPlayers / 1.2f;

        float fielOfView = Mathf.Clamp(camSize, 12, 18f);
        camComponent.orthographicSize = Mathf.Lerp(camComponent.fieldOfView, fielOfView, Time.deltaTime * 100);
    }

}
