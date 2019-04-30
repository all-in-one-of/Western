using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [System.NonSerialized] public bool active;

    public NavMeshAgent navMeshAgent;


    [Header("Phase 1 (run)")]
    public float targetRefreshDelay = 2;

    [Header("Phase 2 (charge)")]
    public float chargeDuration;

    private PlayerBehaviour focusedPlayer;


    


    private IEnumerator RefreshTarget()
    {
        
        float minDist = Mathf.Infinity;
        PlayerBehaviour playerToFocus=null;

        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            NavMeshPath p=new NavMeshPath();
            navMeshAgent.CalculatePath(PlayerManager.instance.players[i].transform.position,p);
            float distance = 0;
            for (int j = 1; j <p.corners.Length; j++)
            {
                distance += Vector3.Distance(p.corners[j - 1], p.corners[j]);
            }

            if (distance < minDist)
            {
                minDist = distance;
                playerToFocus = PlayerManager.instance.players[i];
            }

        }
        if (playerToFocus != null)
        {
            focusedPlayer = playerToFocus;
        }

        yield return new WaitForSeconds(targetRefreshDelay);

        if (active)
        {
            StartCoroutine(RefreshTarget());
        }
    }


    public void Activate(bool on)
    {
        active = on;
        if (active)
        {
            StartCoroutine(RefreshTarget());
        }
        else
        {
            focusedPlayer = null;
        }

    }

    private void Update()
    {
        if (!active) { return; }
        if (focusedPlayer == null) { return; }
        navMeshAgent.SetDestination(focusedPlayer.transform.position);
    }



}
