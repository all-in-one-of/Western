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

    public float groupRange;
    private Enemy enemy;

    [System.NonSerialized] public float distanceToNearestPlayer;


    [Header("Phase 1 (run)")]
    public float targetRefreshDelay = 2;

    private List<EnemyBehaviour> allies;



    private PlayerBehaviour focusedPlayer;
    private bool canSeePlayer = false;
    private bool charging;
    private bool shooting;
    private float currentSpeed;



    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        UpdateSpeed(enemy.moveSpeed);
        spriteRenderer.sprite = enemy.sprite;
    }

    public void UpdateSpeed(float speed)
    {
        currentSpeed = speed;
        navMeshAgent.speed = currentSpeed;
    }

    private void Charge()
    {
        charging = true;
        StartCoroutine(Shoot());
        print("charging");
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
        print("shooting");
        StartCoroutine(StopShooting());
        
    }

    private IEnumerator StopShooting()
    {
        yield return new WaitForSeconds(enemy.shootDuration);
        shooting = false;
        StartCoroutine(RefreshTarget());
    }


    private IEnumerator RefreshTarget()
    {
        yield return new WaitForSeconds(targetRefreshDelay);
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
        distanceToNearestPlayer = minDist;
        if (playerToFocus != null)
        {
            FocusPlayer(playerToFocus);
        }

        

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
        if (focusedPlayer == null || shooting) { return; }
        if (charging)
        {
            self.LookAt(focusedPlayer.self);
        }
        if (Vector3.Distance(self.position,focusedPlayer.self.position)<=enemy.range && canSeePlayer)
        {
            Charge();
        }
        else
        {
            navMeshAgent.SetDestination(focusedPlayer.transform.position);
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
        if (player != null && player==focusedPlayer)
        {
            RaycastHit hit;
            if (Physics.Raycast(self.position,(self.position-player.self.position).normalized,out hit))
            {
                PlayerBehaviour raycastedPlayer = hit.collider.GetComponent<PlayerBehaviour>();
                if (raycastedPlayer==null || raycastedPlayer != focusedPlayer)
                {
                    canSeePlayer = false;
                }
                else
                {
                    canSeePlayer = true;
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
        if (player != null)
        {
            if (player == focusedPlayer)
            {
                canSeePlayer = false;
            }
        }
    }
}
