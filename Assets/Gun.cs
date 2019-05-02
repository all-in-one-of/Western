using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public EnemyBehaviour enemyBehaviour;

    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour player = other.transform.parent.GetComponentInParent<PlayerBehaviour>();
        print("object in trigger : " + other);
        if (player != null)
        {
            enemyBehaviour.playerInTriggerBox = player;
            return;
        }
        

    }


    private void OnTriggerExit(Collider other)
    {
        PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
        if (player != null && player==enemyBehaviour.playerInTriggerBox)
        {
            enemyBehaviour.playerInTriggerBox = null;
        }
    }
}
