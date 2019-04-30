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

        transform.position = (Player1.position + Player2.position) / 2;
        transform.position = new Vector3(transform.position.x- 0.3f, transform.position.y+11.41f, transform.position.z-9.37f);
        
        float camSize = rangeBewteenPlayers / 0.2f;
        Debug.Log(camSize);
        Debug.Log(Mathf.Clamp(camSize, 46.8f, 71f));
        camComponent.fieldOfView = Mathf.Clamp(camSize, 46.8f, 71f);
    }
}
