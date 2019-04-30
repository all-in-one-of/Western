using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //ici ce sont les valeurs au runtime, qu'on actualise directement quand on achète une upgrade
    [System.NonSerialized] public int maxHealthUpgradeLevel;
    [System.NonSerialized] public int speedUpgradeLevel;
    [System.NonSerialized] public int maxAmmoUpgradeLevel;
    [System.NonSerialized] public int startAmmoUpgradeLevel;
    [System.NonSerialized] public int maxStaminaUpgradeLevel;

    [System.NonSerialized] public int credits;
    public Transform self;

    public List<EnemyBehaviour> attackingEnemies;

    private float moveSpeed;

    private void Start()
    {
        attackingEnemies = new List<EnemyBehaviour>();
    }

    public void AddAttackingEnemy(EnemyBehaviour enemy)
    {
        attackingEnemies.Add(enemy);
    }

    public void RemoveAttackingEnemy(EnemyBehaviour enemy)
    {
        attackingEnemies.Remove(enemy);
    }

    private void RefreshEnemiesSpeed()
    {
        //get min and max distance
        float minDist=Mathf.Infinity;
        float maxDist = -Mathf.Infinity;
        for (int i = 0; i < attackingEnemies.Count; i++)
        {
            if (attackingEnemies[i].distanceToNearestPlayer < minDist)
            {
                minDist = attackingEnemies[i].distanceToNearestPlayer;
            }
            else if (attackingEnemies[i].distanceToNearestPlayer > maxDist)
            {
                maxDist = attackingEnemies[i].distanceToNearestPlayer;
            }
        }
        

        for (int i = 0; i < attackingEnemies.Count; i++)
        {
            float percent = Mathf.InverseLerp(minDist, maxDist, attackingEnemies[i].distanceToNearestPlayer);
            moveSpeed = Mathf.Lerp(EnemyManager.instance.minGroupSpeed, EnemyManager.instance.maxGroupSpeed, percent);
        }

    }
}
