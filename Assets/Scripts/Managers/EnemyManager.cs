using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private bool on = false;
    public List<EnemyBehaviour> enemies;

    public GameObject enemyPrefab;

    public float minGroupSpeed;
    public float maxGroupSpeed;


    public void Init()
    {
        
        for (int i = 0; i < EnemySpawn.spawns.Count; i++)
        {
            SpawnEnemy(i);
        }
        Enable(true);
    }


    private void SpawnEnemy(int enemyNumber)
    {
        if (enemyNumber < EnemySpawn.spawns.Count)
        {
            enemies.Add(Instantiate(enemyPrefab, EnemySpawn.spawns[enemyNumber].self.position, EnemySpawn.spawns[enemyNumber].self.rotation).GetComponent<EnemyBehaviour>());
            enemies[enemies.Count - 1].Init(EnemySpawn.spawns[enemyNumber].enemyToSpawn);
        }
        else
        {
            Debug.Log("no more enemy spawn point");
            return;
        }
    }


    public void Enable(bool on)
    {
        this.on = on;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Activate(on);
        }
    }
}
