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
        Debug.Log(rangeBewteenPlayers);

        transform.position = (Player1.position + Player2.position) / 2;
        transform.position = new Vector3(transform.position.x- 0.3f, transform.position.y+26, transform.position.z-58);
        
        float camSize = rangeBewteenPlayers / 2;

        camComponent.orthographicSize = Mathf.Clamp(camSize, 6, 10);
    }
}
