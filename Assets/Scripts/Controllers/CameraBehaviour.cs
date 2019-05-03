using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [System.NonSerialized] public Transform Player1;
    [System.NonSerialized] public Transform Player2;

    public Camera camComponent;

    public int minCamSize;
    public int maxCamSize;

    Vector3 newcamPosition;
    float timer;
    private bool active = false;
    public Vector3 camPosition;

    public void Init()
    {
        Player1 = PlayerManager.instance.players[0].self;
        Player2 = PlayerManager.instance.players[1].self;
        Activate(true);
    }
    
    public void Update()
    {
        if (!active) { return; }
        float rangeBewteenPlayers = Vector3.Distance(Player1.position, Player2.position);
        
        Vector3 positionToGo = (Player1.position + Player2.position) / 2;
        Vector3 positionToTake = positionToGo += (transform.forward * -25);

        transform.position = Vector3.Lerp(transform.position, positionToTake, Time.deltaTime*10);

        float camSize = rangeBewteenPlayers / 1.2f;

        float fielOfView = Mathf.Clamp(camSize, minCamSize, maxCamSize);
        camComponent.orthographicSize = Mathf.Lerp(camComponent.fieldOfView, fielOfView, Time.deltaTime * 100);

    }

    public void Activate(bool on)
    {
        active = on;
    }

}
