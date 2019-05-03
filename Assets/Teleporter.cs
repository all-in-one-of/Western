using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour player= other.GetComponentInParent<PlayerBehaviour>();
        if (player == null)
        {
            player = other.GetComponent<PlayerBehaviour>();
        }

        if (player!=null)
        {
            UIManager.NextArena();
            
        }
    }
}
