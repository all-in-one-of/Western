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

    private void Start()
    {
        attackingEnemies = new List<EnemyBehaviour>();
    }

    public void AddAttackingEnemy(EnemyBehaviour enemy)
    {
        attackingEnemies.Add(enemy);
        RefreshEnemiesSpeed();
    }

    public void RemoveAttackingEnemy(EnemyBehaviour enemy)
    {
        attackingEnemies.Remove(enemy);
        RefreshEnemiesSpeed();
    }

    private void RefreshEnemiesSpeed()
    {
        //get min and max distance
        float maxDist = -Mathf.Infinity;
        for (int i = 0; i < attackingEnemies.Count; i++)
        {
            if (attackingEnemies[i].distanceToNearestPlayer > maxDist)
            {
                maxDist = attackingEnemies[i].distanceToNearestPlayer;
            }
        }
        

        for (int i = 0; i < attackingEnemies.Count; i++)
        {
            float percent = Mathf.InverseLerp(0, maxDist, attackingEnemies[i].distanceToNearestPlayer);
            print("percent : " + percent);
            print(EnemyManager.instance.minGroupSpeed + "," + EnemyManager.instance.maxGroupSpeed);
            attackingEnemies[i].UpdateSpeed(Mathf.Lerp(EnemyManager.instance.minGroupSpeed, EnemyManager.instance.maxGroupSpeed, percent));
            print("new speed : " + Mathf.Lerp(EnemyManager.instance.minGroupSpeed, EnemyManager.instance.maxGroupSpeed, percent));
        }

    }
}
