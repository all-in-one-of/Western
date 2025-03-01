﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [System.NonSerialized] public bool active;

    public NavMeshAgent navMeshAgent;
    public Transform self;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public float groupRange;
    public Gun gun;
    public Transform gunTransform;
    [System.NonSerialized] public Enemy enemy;
    private float currentHealth;

    [System.NonSerialized] public float distanceToNearestPlayer;


    [Header("Phase 1 (run)")]
    public float targetRefreshDelay = 2;

    private List<EnemyBehaviour> allies;
    private bool dead = false;


    private PlayerBehaviour focusedPlayer;
    private bool canSeePlayer = false;
    private bool charging;
    private bool shooting;
    private float currentSpeed;

    [System.NonSerialized] public PlayerBehaviour playerInTriggerBox;



    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        UpdateSpeed(enemy.moveSpeed);
    }

    public void UpdateSpeed(float speed)
    {
        currentSpeed = speed;
        if (!float.IsNaN(speed))
        {
            navMeshAgent.speed = currentSpeed;
        }
    }

    private void Charge()
    {
        navMeshAgent.isStopped = true;
        charging = true;
        StartCoroutine(Shoot());
    }

    private void FocusPlayer(PlayerBehaviour player)
    {
        //on se retire de la liste du joueur focus précédent
        if (focusedPlayer!=null && focusedPlayer.attackingEnemies!=null && focusedPlayer.attackingEnemies.Contains(this))
        {
            focusedPlayer.attackingEnemies.Remove(this);
        }
        
        focusedPlayer = player;
        focusedPlayer.AddAttackingEnemy(this);

    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(enemy.chargeDuration);
        charging = false;
        shooting = true;
        //shoot
        if (!dead)
        {
            gun.ShootOnPlayer(focusedPlayer);
            StartCoroutine(StopShooting());
        }
    }

    private IEnumerator StopShooting()
    {
        yield return new WaitForSeconds(enemy.shootDuration);
        shooting = false;
        StartCoroutine(RefreshTarget());
    }


    private IEnumerator RefreshTarget()
    {
        
        float minDist = Mathf.Infinity;
        PlayerBehaviour playerToFocus=null;

        for (int i = 0; i < PlayerManager.instance.players.Count; i++)
        {
            if (PlayerManager.instance.players[i].controllerBehaviour.data.state == ControllerData.PlayerStates.Alive && Vector3.Distance(self.position,PlayerManager.instance.players[i].self.position)<=enemy.aggroRange)
            {
                NavMeshPath p = new NavMeshPath();

                if (navMeshAgent.isOnNavMesh)
                {
                    navMeshAgent.CalculatePath(PlayerManager.instance.players[i].transform.position, p);
                }


                float distance = 0;
                for (int j = 1; j < p.corners.Length; j++)
                {
                    distance += Vector3.Distance(p.corners[j - 1], p.corners[j]);
                }

                if (distance < minDist)
                {
                    minDist = distance;
                    playerToFocus = PlayerManager.instance.players[i];
                }
            }
            

        }
        distanceToNearestPlayer = minDist;
        if (playerToFocus != null)
        {
            FocusPlayer(playerToFocus);
        }

        yield return new WaitForSeconds(targetRefreshDelay);

        if (active && !dead)
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
        if (dead) { return; }
        if (focusedPlayer == null || shooting) { return; }
        
        gunTransform.eulerAngles = new Vector3(gunTransform.eulerAngles.x, gunTransform.eulerAngles.y, Quaternion.LookRotation(focusedPlayer.self.position - self.position, Vector3.up).eulerAngles.y + 90);


        if (gunTransform.eulerAngles.z >= 90 && gunTransform.eulerAngles.z < 270)
        {
            gun.spriteRenderer.flipY = true;
            spriteRenderer.flipX = true;
        }
        else
        {
            gun.spriteRenderer.flipY = false;
            spriteRenderer.flipX = false;
        }

        //Orientation
        if (gunTransform.eulerAngles.z <= 360 && gunTransform.eulerAngles.z > 180)
        {
            animator.SetBool("GoingUp", false);
            gun.spriteRenderer.sortingOrder = -50;
        }
        else
        {
            animator.SetBool("GoingUp", true);
            gun.spriteRenderer.sortingOrder = 50;
        }




        if (charging)
        {
            //self.LookAt(focusedPlayer.self);
            
            
            return;
        }

        if (playerInTriggerBox != null && playerInTriggerBox==focusedPlayer)
        {
            RaycastHit hit;
            int layerMask = (1 << 12)|(1<<11);
            layerMask = ~layerMask;
            if (Physics.Raycast(self.position, playerInTriggerBox.self.position-self.position, out hit,Mathf.Infinity,layerMask))
            {
                PlayerBehaviour raycastedPlayer = hit.collider.GetComponentInParent<PlayerBehaviour>();
                if (raycastedPlayer == null || raycastedPlayer != focusedPlayer)
                {
                    canSeePlayer = false;
                }
                else
                {
                    canSeePlayer = true;
                }
            }
        }

        //velocity
        if (Vector3.Distance(self.position,focusedPlayer.self.position)<=enemy.range && canSeePlayer)
        {
            Charge();
            animator.SetBool("Moving", false);
        }
        else
        {
            if (navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(focusedPlayer.transform.position);
                animator.SetBool("Moving", true);
            } 
        }
    }


    public void Die()
    {
        GetComponentInChildren<CapsuleCollider>().enabled = false;
        EnemyManager.instance.RemoveEnemy(this);
        focusedPlayer.attackingEnemies.Remove(this);
        animator.SetTrigger("Dead");
        dead = true;
        navMeshAgent.isStopped = true;
        
    }
}
